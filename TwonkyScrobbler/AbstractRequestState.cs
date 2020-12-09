using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TwonkyScrobbler
{
    internal class AbstractRequestState
    {
        public HttpWebRequest Request;
        public HttpWebResponse Response;

        public AbstractRequestState()
        {
        }
    }
}
