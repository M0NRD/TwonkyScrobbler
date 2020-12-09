using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using TwonkyScrobbler.SongReader;

namespace TwonkyScrobbler
{
    public class ScrobblerConfigurationSection : ConfigurationSection
    {
        static ConfigurationProperty passwordProperty;
        static ConfigurationProperty balloonProperty;
        static ConfigurationProperty usernameProperty;
        static ConfigurationProperty feedProperty;
        static ConfigurationProperty serverProperty;
        static ConfigurationProperty lastsubmissionProperty;

        static ConfigurationPropertyCollection properties;

        static ScrobblerConfigurationSection()
        {
            usernameProperty = new ConfigurationProperty("username", typeof(string),
                            null, ConfigurationPropertyOptions.None);

            passwordProperty = new ConfigurationProperty("password", typeof(string),
                            null, ConfigurationPropertyOptions.None);

            balloonProperty = new ConfigurationProperty("balloon", typeof(bool),
                            true, ConfigurationPropertyOptions.None);

            feedProperty = new ConfigurationProperty("feed", typeof(string),
                            null, ConfigurationPropertyOptions.None);

            serverProperty = new ConfigurationProperty("server", typeof(string),
                            null, ConfigurationPropertyOptions.None);

            lastsubmissionProperty = new ConfigurationProperty("lastsubmission", typeof(DateTime),
                            null, ConfigurationPropertyOptions.None);

            properties = new ConfigurationPropertyCollection();

            properties.Add(balloonProperty);
            properties.Add(usernameProperty);
            properties.Add(passwordProperty);
            properties.Add(feedProperty);
            properties.Add(serverProperty);
            properties.Add(lastsubmissionProperty);
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return properties; }
        }

        #region Configuration properties

        [ConfigurationProperty("username", IsRequired = false)]
        public string Username
        {
            get { return (string)base[usernameProperty]; }
            set { base[usernameProperty] = value; }
        }

        [ConfigurationProperty("balloon", IsRequired = false)]
        public bool Balloon
        {
            get { return (bool)base[balloonProperty]; }
            set { base[balloonProperty] = value; }
        }


        [ConfigurationProperty("password", IsRequired = false)]
        public string Password
        {
            get { return (string)base[passwordProperty]; }
            set { base[passwordProperty] = value; }
        }

        [ConfigurationProperty("feed", IsRequired = false)]
        public string Feed
        {
            get { return (string)base[feedProperty]; }
            set { base[feedProperty] = value; }
        }

        [ConfigurationProperty("server", IsRequired = false)]
        public string Server
        {
            get { return (string)base[serverProperty]; }
            set { base[serverProperty] = value; }
        }

        [ConfigurationProperty("lastsubmission", IsRequired = false)]
        public DateTime LastSubmission
        {
            get { return (DateTime)base[lastsubmissionProperty]; }
            set { base[lastsubmissionProperty] = value; }
        }

        #endregion

        #region Properties

        public static ConfigurationProperty UsernameProperty
        {
            get { return ScrobblerConfigurationSection.usernameProperty; }
        }

        public static ConfigurationProperty PasswordProperty
        {
            get { return ScrobblerConfigurationSection.passwordProperty; }
        }

        public static ConfigurationProperty BalloonProperty
        {
            get { return ScrobblerConfigurationSection.balloonProperty; }
        }

        public static ConfigurationProperty FeedProperty
        {
            get { return ScrobblerConfigurationSection.feedProperty; }
        }

        public static ConfigurationProperty ServerProperty
        {
            get { return ScrobblerConfigurationSection.serverProperty; }
        }

        public static ConfigurationProperty LastSubmissionProperty
        {
            get { return ScrobblerConfigurationSection.lastsubmissionProperty; }
        }
        #endregion
    }
}
