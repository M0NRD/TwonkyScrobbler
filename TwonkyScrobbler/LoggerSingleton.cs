using System;
using System.Collections.Generic;
using System.Text;

using log4net;
using log4net.Config;

namespace TwonkyScrobbler
{
    class LoggerSingleton
    {
        private static ILog log;

        public static ILog Logger
        {
            get
            {
                if (log == null)
                {
                    log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    XmlConfigurator.Configure();
                }
                return log;
            }
        }
    }
}
