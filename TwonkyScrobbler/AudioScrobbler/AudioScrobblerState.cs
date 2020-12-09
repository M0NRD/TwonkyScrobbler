using System;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.AudioScrobbler
{
    enum AudioScrobblerState
    {
        Idle,
        NeedHandshake,
        NeedTransmit,
        WaitingForRequestStream,
        WaitingForHandshakeResponse,
        WaitingForResponse
    }
}
