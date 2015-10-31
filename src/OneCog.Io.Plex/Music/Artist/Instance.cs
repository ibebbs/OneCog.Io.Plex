using System;

namespace OneCog.Io.Plex.Music.Artist
{
    internal class Instance : IArtist
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public Uri Art { get; set; }

        public Uri Thumb { get; set; }
    }
}
