using System;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.SongReader
{
    public class SongReadEventArgs : EventArgs
    {
        SongInformation song;

        public SongReadEventArgs()
        {
        }

        public SongReadEventArgs(SongInformation song)
        {
            Song = song;
        }

        public SongInformation Song
        {
            get { return song; }
            set { song = value; }
        }

        public override string ToString()
        {
            return song.ToString();
        }        
    }
}
