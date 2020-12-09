using System;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.AudioScrobbler
{
    public class AudioScrobblerStatusEventArgs : EventArgs
    {
        string status;

        public AudioScrobblerStatusEventArgs()
        {
        }

        public AudioScrobblerStatusEventArgs(string status)
        {
            Status = status;
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }


    }
}
