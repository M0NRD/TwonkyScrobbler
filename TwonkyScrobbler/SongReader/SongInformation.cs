using System;
using System.Collections.Generic;
using System.Text;

namespace TwonkyScrobbler.SongReader
{
    public class SongInformation
    {
        string title;
        string album;
        string artist;
        DateTime startTime;

        public SongInformation(string title, string album, string artist, DateTime startTime)
        {
            Title = title;
            Album = album;
            Artist = artist;
            StartTime = startTime;
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public static bool operator ==(SongInformation a, SongInformation b)
        {
            try
            {
                return a.Album == b.Album && a.Artist == b.Artist && a.Title == b.Title;
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(SongInformation a, SongInformation b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return String.Format("Artist: {0}; Title: {1}; Album: {2}", Artist, Title, Album);
        }

        public override bool Equals(object obj)
        {
            if(! (obj is SongInformation)) return false;
            SongInformation b = obj as SongInformation;
            return b == this;
        }

        public override int GetHashCode()
        {
            if (artist == "" || title == "" || album == "") return 0;
            return artist[0] + 10 * title[0] + 100 * album[0];
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        public string Album
        {
            get { return album; }
            set { album = value; }
        }
    }
}
