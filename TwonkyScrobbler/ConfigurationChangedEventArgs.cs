using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace TwonkyScrobbler
{
    public class ConfigurationChangedEventArgs : EventArgs
    {
        ConfigurationProperty configurationProperty;

        public ConfigurationChangedEventArgs()
        {

        }

        public ConfigurationChangedEventArgs(ConfigurationProperty configurationProperty)
        {
            this.configurationProperty = configurationProperty;
        }

        public ConfigurationProperty ConfigurationProperty
        {
            get { return configurationProperty; }
            set { configurationProperty = value; }
        }
    }
}
