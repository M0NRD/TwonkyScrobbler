using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.SongReader
{
    internal class PollingSongReader : SongReader
    {
        Timer timer = new Timer();
        bool started = false;
        bool gotResponse = false;

        public PollingSongReader()
        {
            SongRead += new SongReadEventHandler(songRead);
            SongReadFailed += new SongReadFailedEventHandler(songReadFailed);

            timer.Interval = 30 * 1000;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        public void Start()
        {
            ReadSong();
            started = true;
            gotResponse = false;
            timer.Start();
        }

        public void Stop()
        {
            started = false;
            timer.Stop();
        }

        void songRead(object sender, SongReadEventArgs e)
        {
            gotResponse = true;
        }

        void songReadFailed(object sender, SongReadFailedEventArgs e)
        {
            gotResponse = true;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (gotResponse)
            {
                ReadSong();
            }
            if (started) Start();
        }
    }
}
