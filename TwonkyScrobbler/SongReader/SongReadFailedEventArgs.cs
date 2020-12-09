using System;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.SongReader
{
    public class SongReadFailedEventArgs : EventArgs
    {
        string message;

        public SongReadFailedEventArgs()
        {
        }

        public SongReadFailedEventArgs(string message)
        {
            this.Message = message;
        }
        
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
