using System;
using System.Configuration;

using TwonkyScrobbler;

namespace TwonkyScrobbler
{
    public delegate void ConfigurationChangedEventHandler(object sender, ConfigurationChangedEventArgs e);

    public class ConfigurationSingleton
    {
        static Configuration config;
        static ScrobblerConfigurationSection section;

        private ConfigurationSingleton()
        {
        }

        public static event ConfigurationChangedEventHandler ConfigurationChanged;

        private static ScrobblerConfigurationSection Section
        {
            get
            {
                if (config == null)
                {
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    section = config.GetSection("TwonkyScrobbler") as ScrobblerConfigurationSection;
                }
                
                return section;
            }
        }

        public static string Username
        {
            get { return Section.Username; }
            set
            {
                Section.Username = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.UsernameProperty);
            }
        }

        public static string Password
        {
            get { return Section.Password; }
            set
            {
                Section.Password = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.PasswordProperty);
            }
        }

        public static string Feed
        {
            get { return Section.Feed; }
            set
            {
                Section.Feed = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.FeedProperty);
            }
        }

        public static string Server
        {
            get { return Section.Server; }
            set
            {
                Section.Server = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.ServerProperty);
            }
        }

        public static bool Balloon
        {
            get { return Section.Balloon; }
            set
            {
                Section.Balloon = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.BalloonProperty);
            }
        }

        public static DateTime LastSubmission
        {
            get { return Section.LastSubmission; }
            set
            {
                Section.LastSubmission = value;
                OnConfigurationChanged(ScrobblerConfigurationSection.LastSubmissionProperty);
            }
        }

        protected static void OnConfigurationChanged(ConfigurationProperty configurationProperty)
        {
            if(ConfigurationChanged != null)
            {
                ConfigurationChanged(null, new ConfigurationChangedEventArgs(configurationProperty));
            }
        }

        public static void Save()
        {
            ScrobblerConfigurationSection section = Section;
            config.Save();
        }
    }
}