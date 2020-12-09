using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;

using TwonkyScrobbler;

namespace TwonkyScrobbler.SongReader
{
    delegate void SongReadEventHandler(object sender, SongReadEventArgs e);
    delegate void SongReadFailedEventHandler(object sender, SongReadFailedEventArgs e);
    
    class SongReader
    {
        const int bufferSize = 1024;
        public string Feed = "http://localhost:9000/rss/feed/1$11$207905894.xml";

        public event SongReadEventHandler SongRead;
        public event SongReadFailedEventHandler SongReadFailed;
        public SongInformation lastScrobbledSong = new SongInformation ("song","album","artist", DateTime.Now);

        private DateTime ProgramStartTime = DateTime.Now;        

        Regex rssSongInformationRegex = new Regex("\\<title\\>(.*?)\\</title\\>\\s\\<link\\>(.*?)\\</link\\>\\s\\<description\\>(.*?)\\</description\\>\\s\\<author\\>(.*?)\\</author\\>\\s\\<enclosure url=.*?/>\\s\\<pubDate\\>.*?\\</pubDate\\>\\s\\<moddatetime\\>.*?\\</moddatetime\\>\\s\\<playeddatetime\\>(.*?)\\</playeddatetime\\>");

        Regex getSongInformationRegex = new Regex("\\d+:\\d+ (.*?) - (.*?)\\</xml\\>", RegexOptions.Compiled);

        public void ReadSong()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Feed);

                RequestState requestState = new RequestState();
                requestState.Request = request;

                IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(respCallback), requestState);
                if (requestState.Response != null)
                {
                    requestState.Response.Close();
                }
            }
            catch (WebException e)
            {
                OnSongReadFailed(new SongReadFailedEventArgs(e.Message));
            }
            catch (Exception e)
            {
                OnSongReadFailed(new SongReadFailedEventArgs(e.Message));
            }
        }

        private void respCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                RequestState requestState = (RequestState)asynchronousResult.AsyncState;
                HttpWebRequest request = requestState.Request;
                requestState.Response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                Stream responseStream = requestState.Response.GetResponseStream();
                requestState.StreamResponse = responseStream;

                IAsyncResult asynchronousInputRead = responseStream.BeginRead(requestState.BufferRead, 0, bufferSize, new AsyncCallback(readCallBack), requestState);
                return;
            }
            catch (WebException e)
            {
                OnSongReadFailed(new SongReadFailedEventArgs(e.Message));
            }
        }

        private void readCallBack(IAsyncResult asyncResult)
        {
            try
            {
                RequestState requestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = requestState.StreamResponse;
                int read = responseStream.EndRead(asyncResult);

                if (read > 0)
                {
                    requestState.RequestData.Append(Encoding.ASCII.GetString(requestState.BufferRead, 0, read));
                    IAsyncResult asynchronousResult = responseStream.BeginRead(requestState.BufferRead, 0, bufferSize, new AsyncCallback(readCallBack), requestState);
                    return;
                }
                else
                {
                    
                    string title = "";
                    string link = "";
                    string album = "";
                    string artist = "";
                    string time = "";

                    if (requestState.RequestData.Length > 1)
                    {
                        string stringContent = requestState.RequestData.ToString();
                        Match m;
                        
                        // Process the xml till reach bottom...  
                        for (m = rssSongInformationRegex.Match(stringContent); m.Success; m = m.NextMatch())
                        {
                            title = m.Groups[1].Value;
                            link = m.Groups[2].Value;
                            album = m.Groups[3].Value;
                            artist = m.Groups[4].Value;
                            time = m.Groups[5].Value;
                        }

                        DateTime playedtime = DateTime.Parse(time); //convert format

                        if (DateTime.Compare(playedtime, lastScrobbledSong.StartTime) > 0) // if played time > last scrobbled song
                        {
                            OnSongRead(new SongReadEventArgs(new SongInformation(title, album, artist, playedtime)));
                        }
                    }
                    responseStream.Close();
                }
            }
            catch (WebException e)
            {
                OnSongReadFailed(new SongReadFailedEventArgs(e.Message));
            }
        }

        protected void OnSongReadFailed(SongReadFailedEventArgs e)
        {
            if (SongReadFailed != null)
            {
                SongReadFailed(this, e);
            }
        }

        protected void OnSongRead(SongReadEventArgs e)
        {
            if (SongRead != null)
            {
                SongRead(this, e);
            }
        }
    }
}