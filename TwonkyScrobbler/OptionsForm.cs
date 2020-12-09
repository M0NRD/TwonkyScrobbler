using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace TwonkyScrobbler
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            usernameTextBox.Text = ConfigurationSingleton.Username;
            passwordTextBox.Text = ConfigurationSingleton.Password;
            showBallonTipsCheckBox.Checked = ConfigurationSingleton.Balloon;
            urlHost.Text = ConfigurationSingleton.Server;
            UrlFeed.Text = ConfigurationSingleton.Feed;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ConfigurationSingleton.Username = usernameTextBox.Text;
            ConfigurationSingleton.Password = passwordTextBox.Text;
            ConfigurationSingleton.Balloon = showBallonTipsCheckBox.Checked;
            ConfigurationSingleton.Feed = UrlFeed.Text;
            ConfigurationSingleton.Server = urlHost.Text;
            
            ConfigurationSingleton.Save();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}