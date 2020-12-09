using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.SongReader
{
    internal class RequestState : AbstractRequestState
    {
        const int bufferSize = 1024;
        public StringBuilder RequestData;
        public byte[] BufferRead;
        public Stream StreamResponse;

        public RequestState()
        {
            BufferRead = new byte[bufferSize];
            RequestData = new StringBuilder();
        }
    }
}
