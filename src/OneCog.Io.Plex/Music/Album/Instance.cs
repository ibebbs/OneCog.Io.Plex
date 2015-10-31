using System;

namespace OneCog.Io.Plex.Music.Album
{
    internal class Instance : IAlbum
    {
        public string Key { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Studio { get; set; }

        public string ArtistKey { get; set; }

        public int Year { get; set; }

        public Uri Art { get; set; }

        public Uri Thumb { get; set; }
    }
}
