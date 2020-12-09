using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using TwonkyScrobbler.AudioScrobbler;
using TwonkyScrobbler.SongReader;

namespace TwonkyScrobbler
{
    public partial class Scrobbler : Form
    {
        PollingSongReader SongReader;
        AudioScrobblerEngine ScrobblerEngine;

        public string serverUri = "http://localhost:9000";
        const string supportUri = "http://www.twonkyvision.com";

        public Scrobbler()
        {
            InitializeComponent();
            LoggerSingleton.Logger.Info("Starting...");
            
            SongReader = new PollingSongReader();
            SongReader.Feed = ConfigurationSingleton.Feed;
            SongReader.SongRead += new SongReadEventHandler(SongReader_SongRead);
            SongReader.SongReadFailed += new SongReadFailedEventHandler(SongReader_SongReadFailed);

            SongReader.lastScrobbledSong.StartTime = ConfigurationSingleton.LastSubmission;

            ScrobblerEngine = new AudioScrobblerEngine();
            ScrobblerEngine.ScrobblerStatusChanged += new AudioScrobblerStatusEventHandler(ScrobblerEngine_ScrobblerStatusChanged);
            setAudioScrobblerCredentials();

            ConfigurationSingleton.ConfigurationChanged += new ConfigurationChangedEventHandler(ConfigurationSingleton_ConfigurationChanged);

            usernameText.Text = ConfigurationSingleton.Username;
            urlText.Text = ConfigurationSingleton.Feed;            
            statusText.Text = "Running...";
            Load += new EventHandler(MainForm_Load);
        }

        void ConfigurationSingleton_ConfigurationChanged(object sender, ConfigurationChangedEventArgs e)
        {
            if(e.ConfigurationProperty.Name == "username" || e.ConfigurationProperty.Name == "password")
            {
                setAudioScrobblerCredentials();
            }
            else if (e.ConfigurationProperty.Name == "feed")
            {
                SongReader.Feed = ConfigurationSingleton.Feed;
            }
            else if (e.ConfigurationProperty.Name == "server")
            {
                serverUri = ConfigurationSingleton.Server;
            }
            else if (e.ConfigurationProperty.Name == "lastsubmission")
            {
                SongReader.lastScrobbledSong.StartTime = ConfigurationSingleton.LastSubmission;
            }

            usernameText.Text = ConfigurationSingleton.Username;
            urlText.Text = ConfigurationSingleton.Feed;
            statusText.Text = "Running...";            
        }

        void ScrobblerEngine_ScrobblerStatusChanged(object sender, AudioScrobblerStatusEventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
            {
                statusText.Text = e.Status;
            }
            ));
        }

        private void setAudioScrobblerCredentials()
        {
            ScrobblerEngine.SetUserPassword(ConfigurationSingleton.Username, ConfigurationSingleton.Password);
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            SongReader.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ScrobblerEngine.Stop();

            ConfigurationSingleton.ConfigurationChanged -= ConfigurationSingleton_ConfigurationChanged;

            SongReader.SongRead -= SongReader_SongRead;
            SongReader.SongReadFailed -= SongReader_SongReadFailed;
            SongReader.Stop();
        }

        void SongReader_SongReadFailed(object sender, SongReadFailedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
            {
                statusText.Text = e.Message;
                titleText.Text = "";
                albumText.Text = "";
                artistText.Text = "";
                timeText.Text = "";
            }
            ));
        }

        void SongReader_SongRead(object sender, SongReadEventArgs e)
        {
            Invoke(new MethodInvoker(delegate()
            {
                titleText.Text = e.Song.Title;
                albumText.Text = e.Song.Album;
                artistText.Text = e.Song.Artist;
                timeText.Text = e.Song.StartTime.ToString();

                if (e.Song != SongReader.lastScrobbledSong)
                {
                    System.Diagnostics.Debug.WriteLine("scrobbling " + e.Song);
                    ScrobblerEngine.Enqueue(e.Song);
                    SongReader.lastScrobbledSong = e.Song;
                    ConfigurationSingleton.LastSubmission = e.Song.StartTime;
                    ConfigurationSingleton.Save();

                    string details = e.Song.Artist + " - " + e.Song.Title + " (" + e.Song.Album + ")";
                    if (details.Length > 63)
                    {
                        details = details.Substring(0, 60) + "...";
                    }
                    notifyIcon.Text = details;

                    if (ConfigurationSingleton.Balloon)
                    {
                        notifyIcon.BalloonTipTitle = e.Song.Title;
                        notifyIcon.BalloonTipText = e.Song.Artist + Environment.NewLine + e.Song.Album;
                        notifyIcon.ShowBalloonTip(2000);
                    }
                }
            }
            ));
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog();
        }

        private void playListButton_Click(object sender, EventArgs e)
        {
            openWebpage(serverUri+"/rss");
        }

        private void openWebpage(string uri)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = uri;
            psi.UseShellExecute = true;
            Process.Start(psi);
        }

        private void supportButton_Click(object sender, EventArgs e)
        {
            openWebpage(supportUri);
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Visible = !Visible;
            if (Visible)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
                Win32Interop.SetForegroundWindow(Handle);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
            }
        }

        private void urlText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openWebpage(SongReader.Feed);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            openWebpage("log-file.txt");
        }
    }
}