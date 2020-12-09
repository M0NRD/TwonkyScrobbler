using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace TwonkyScrobbler.AudioScrobbler
{
    internal class AudioScrobblerRequestState : AbstractRequestState
    {
        public Stream StreamResponse;
        public AudioScrobblerTransmitState TransmitState;

        public AudioScrobblerRequestState()
        {
        }
    }
}
