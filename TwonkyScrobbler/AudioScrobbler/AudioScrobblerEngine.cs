/***************************************************************************
 *  Copyright (C) 2005 Novell
 *  Written by Chris Toshok (toshok@ximian.com)
 *  Modified by Markus Palme (2007)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW:
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Net;
using System.Security.Cryptography;
using System.Globalization;

using TwonkyScrobbler;
using TwonkyScrobbler.SongReader;

namespace TwonkyScrobbler.AudioScrobbler
{
    public delegate void AudioScrobblerStatusEventHandler(object sender, AudioScrobblerStatusEventArgs e);

    internal class AudioScrobblerEngine
    {
        const int tickInterval = 10 * 1000;
        const int failureLogMinutes = 3;
        const int retrySeconds = 60;
        const int songDuration = 180;

        const string clientId = "bsh";
        const string clientVersion = "1.0";
        const string scrobblerUrl = "http://post.audioscrobbler.com/";
        const string scrobblerVersion = "1.1";
        
        string username;
        string md5Pass;
        string postUrl;
        string securityToken;
        long waitingForRequestStreamStartTicks;
        
        DateTime nextInterval;
        DateTime lastUploadFailedLogged;

        Queue<SongInformation> queue;
        AudioScrobblerState state;

        Timer timer = new Timer();

        public event AudioScrobblerStatusEventHandler ScrobblerStatusChanged;

        public AudioScrobblerEngine()
        {
            timer.Interval = tickInterval;
            timer.Elapsed += new ElapsedEventHandler(stateMachine);

            state = AudioScrobblerState.Idle;
            queue = new Queue<SongInformation>();
        }

        private void StartTransitionHandler()
        {
            timer.Start();
        }

        public void Stop()
        {
            stopTransitionHandler();
        }

        private void stopTransitionHandler()
        {
            timer.Stop();
        }

        public void SetUserPassword(string username, string pass)
        {
            if (username == "" || pass == "")
                return;

            this.username = username;
            this.md5Pass = md5Encode(pass);

            if (securityToken != null)
            {
                securityToken = null;
                state = AudioScrobblerState.NeedHandshake;
            }
        }

        private string md5Encode(string pass)
        {
            if (pass == null || pass == String.Empty) return String.Empty;

            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(pass));
            return toHex(hash).ToLower();
        }

        private string toHex(byte[] input)
        {
            StringBuilder sb = new StringBuilder(input.Length * 2);

            foreach (byte b in input)
            {
                sb.Append(b.ToString("X2", CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        public void Enqueue(SongInformation song)
        {
            queue.Enqueue(song);
            LoggerSingleton.Logger.InfoFormat("Enqueued {0}", song);
            StartTransitionHandler();
        }

        void stateMachine(object sender, EventArgs e)
        {
            switch (state)
            {
                case AudioScrobblerState.Idle:
                    if (queue.Count > 0)
                    {
                        if (username != null && md5Pass != null && securityToken == null)
                            state = AudioScrobblerState.NeedHandshake;
                        else
                            state = AudioScrobblerState.NeedTransmit;
                    }
                    else
                    {
                        stopTransitionHandler();
                    }
                    break;

                case AudioScrobblerState.NeedHandshake:
                    if (DateTime.Now > nextInterval)
                    {
                        handshake();
                    }
                    break;

                case AudioScrobblerState.NeedTransmit:
                    if (DateTime.Now > nextInterval)
                    {
                        transmitQueue();
                    }
                    break;
                
                case AudioScrobblerState.WaitingForRequestStream:
                case AudioScrobblerState.WaitingForResponse:
                case AudioScrobblerState.WaitingForHandshakeResponse:
                    break;
            }
        }

        private string getTransmitInfo(out int numTracks)
        {
            StringBuilder sb = new StringBuilder();
            SongInformation[] songs = queue.ToArray();
            int i;
            for (i = 0; i < queue.Count; i++)
            {
                if (i == 9) break;

                SongInformation track = songs[i];
                DateTime startTimeCorrected = track.StartTime.ToUniversalTime();

                sb.AppendFormat(
                         "&a[{6}]={0}&t[{6}]={1}&b[{6}]={2}&m[{6}]={3}&l[{6}]={4}&i[{6}]={5}",
                         HttpUtility.UrlEncode(track.Artist),
                         HttpUtility.UrlEncode(track.Title),
                         HttpUtility.UrlEncode(track.Album),
                         "",
                         songDuration.ToString(),
                         HttpUtility.UrlEncode(startTimeCorrected.ToString("yyyy-MM-dd HH:mm:ss")),
                         i);
            }

            numTracks = i;
            return sb.ToString();
        }

        void transmitQueue()
        {
            int numTracksTransmitted;
            nextInterval = DateTime.MinValue;

            if (postUrl == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("u={0}&s={1}", HttpUtility.UrlEncode(username), securityToken);
            sb.Append(getTransmitInfo(out numTracksTransmitted));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            
            AudioScrobblerRequestState requestState = new AudioScrobblerRequestState();
            requestState.Request = request;

            AudioScrobblerTransmitState ts = new AudioScrobblerTransmitState();
            ts.Count = numTracksTransmitted;
            ts.StringBuilder = sb;
            requestState.TransmitState = ts;

            LoggerSingleton.Logger.InfoFormat("Transmitting {0} songs", ts.Count);

            state = AudioScrobblerState.WaitingForRequestStream;

            IAsyncResult asyncResult = (IAsyncResult)request.BeginGetRequestStream(new AsyncCallback(transmitGetRequestStream), requestState);
            if (requestState.Response != null)
            {
                requestState.Response.Close();
            }

            waitingForRequestStreamStartTicks = DateTime.Now.Ticks;

            if (asyncResult == null)
            {
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                state = AudioScrobblerState.Idle;
            }
        }

        private void transmitGetRequestStream(IAsyncResult ar)
        {
            AudioScrobblerRequestState requestState = (AudioScrobblerRequestState)ar.AsyncState;
            HttpWebRequest request = requestState.Request;
            Stream stream;
            
            LoggerSingleton.Logger.InfoFormat("Posting song information");

            try
            {
                stream = request.EndGetRequestStream(ar);
            }
            catch (Exception e)
            {
                OnAudioScrobblerStatusChanged("Failed to get the request stream: " + e.Message);
                LoggerSingleton.Logger.InfoFormat("Failed to get the request stream: {0}", e.Message);

                state = AudioScrobblerState.Idle;
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                return;
            }

            requestState.StreamResponse = stream;

            StringBuilder sb = requestState.TransmitState.StringBuilder;
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(sb.ToString());
            writer.Close();
            stream.Close();

            state = AudioScrobblerState.WaitingForResponse;

            LoggerSingleton.Logger.InfoFormat("Requesting response");

            IAsyncResult respResult = request.BeginGetResponse(transmitGetResponse, requestState);

            if (respResult == null)
            {
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                state = AudioScrobblerState.Idle;
            }
        }

        private void transmitGetResponse(IAsyncResult ar)
        {
            AudioScrobblerRequestState requestState = (AudioScrobblerRequestState)ar.AsyncState;
            HttpWebRequest request = requestState.Request;

            WebResponse response = request.EndGetResponse(ar);
            Stream responseStream = response.GetResponseStream();
            
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            string line = sr.ReadLine();

            LoggerSingleton.Logger.InfoFormat("Server response {0}", line);

            DateTime now = DateTime.Now;
            if (line.StartsWith("FAILED"))
            {
                if (now - lastUploadFailedLogged > TimeSpan.FromMinutes(failureLogMinutes))
                {
                    OnAudioScrobblerStatusChanged("Audioscrobbler upload failed: " + line);
                    lastUploadFailedLogged = now;
                }
                state = AudioScrobblerState.NeedTransmit;
            }
            else if (line.StartsWith("BADUSER") || line.StartsWith("BADAUTH"))
            {
                if (now - lastUploadFailedLogged > TimeSpan.FromMinutes(failureLogMinutes))
                {
                    OnAudioScrobblerStatusChanged("Audioscrobbler upload failed: " + line);
                    lastUploadFailedLogged = now;
                }
                securityToken = null;
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                state = AudioScrobblerState.Idle;
                return;
            }
            else if (line.StartsWith("OK"))
            {
                if (lastUploadFailedLogged != DateTime.MinValue)
                {
                    OnAudioScrobblerStatusChanged("Audioscrobbler upload succeeded");
                    lastUploadFailedLogged = DateTime.MinValue;
                }

                for (int i = 0; i < requestState.TransmitState.Count; i++)
                {
                    queue.Dequeue();
                }
                state = AudioScrobblerState.Idle;
            }
            else
            {
                if (now - lastUploadFailedLogged > TimeSpan.FromMinutes(failureLogMinutes))
                {
                    OnAudioScrobblerStatusChanged("Audioscrobbler upload failed: " + line);
                    LoggerSingleton.Logger.Error("Audioscrobbler upload failed: " + line);
                    lastUploadFailedLogged = now;
                }
                state = AudioScrobblerState.Idle;
            }

            line = sr.ReadLine();

            if (line.StartsWith("INTERVAL"))
            {
                int intervalSeconds = Int32.Parse(line.Substring("INTERVAL".Length));
                nextInterval = DateTime.Now + new TimeSpan(0, 0, intervalSeconds);
            }
            else
            {
                OnAudioScrobblerStatusChanged("expected INTERVAL..");
            }

            sr.Close();
            responseStream.Close();
        }

        void handshake()
        {
            LoggerSingleton.Logger.Info("Handshaking...");

            string uri = String.Format("{0}?hs=true&p={1}&c={2}&v={3}&u={4}",
                                        scrobblerUrl,
                                        scrobblerVersion,
                                        clientId, clientVersion,
                                        HttpUtility.UrlEncode(username));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            
            state = AudioScrobblerState.WaitingForHandshakeResponse;

            AudioScrobblerRequestState requestState = new AudioScrobblerRequestState();
            requestState.Request = request;

            IAsyncResult currentAsyncResult = request.BeginGetResponse(handshakeGetResponse, requestState);
            if (currentAsyncResult == null)
            {
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                state = AudioScrobblerState.Idle;
            }
        }

        protected void OnAudioScrobblerStatusChanged(string status)
        {
            if (ScrobblerStatusChanged != null)
            {
                ScrobblerStatusChanged(this, new AudioScrobblerStatusEventArgs(status));
            }
        }

        private void handshakeGetResponse(IAsyncResult ar)
        {
            bool success = false;
            WebResponse resp;

            AudioScrobblerRequestState requestState = (AudioScrobblerRequestState)ar.AsyncState;
            HttpWebRequest request = requestState.Request;

            try
            {
                resp = request.EndGetResponse(ar);
            }
            catch (Exception e)
            {
                OnAudioScrobblerStatusChanged(String.Format("failed to handshake: {0}", e.Message));

                state = AudioScrobblerState.Idle;
                nextInterval = DateTime.Now + new TimeSpan(0, 0, retrySeconds);
                return;
            }

            Stream s = resp.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.UTF8);

            string line = sr.ReadLine();
            LoggerSingleton.Logger.InfoFormat("Handshake response: {0}", line);

            if (line.StartsWith("FAILED"))
            {
                OnAudioScrobblerStatusChanged("Audioscrobbler sign-on failed: " + line);
            }
            else if (line.StartsWith("BADUSER"))
            {
                OnAudioScrobblerStatusChanged("Audioscrobbler sign-on failed: unrecognized user/password (" + line + ")");
            }
            else if (line.StartsWith("UPDATE"))
            {
                OnAudioScrobblerStatusChanged(String.Format("Audioscrobbler plugin needs updating : {0}", line));
                success = true;
            }
            else if (line.StartsWith("UPTODATE"))
            {
                success = true;
            }

            if (success == true)
            {
                string challenge = sr.ReadLine().Trim();
                postUrl = sr.ReadLine().Trim();

                securityToken = md5Encode(md5Pass + challenge);
            }

            line = sr.ReadLine();
            if (line.StartsWith("INTERVAL"))
            {
                int intervalSeconds = Int32.Parse(line.Substring("INTERVAL".Length));
                nextInterval = DateTime.Now + new TimeSpan(0, 0, intervalSeconds);
            }

            state = success ? AudioScrobblerState.Idle : AudioScrobblerState.NeedHandshake;
        }
    }
}
