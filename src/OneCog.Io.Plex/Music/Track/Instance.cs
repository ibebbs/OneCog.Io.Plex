using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Track
{
    internal class Instance : ITrack
    {
        public string Key { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public int Year { get; set; }

        public int Duration { get; set; }

        public Uri Thumb { get; set; }

        public Uri Art { get; set; }

        public string AlbumKey { get; set; }

        public string AlbumTitle { get; set; }

        public Uri AlbumThumb { get; set; }

        public string ArtistKey { get; set; }

        public string ArtistName { get; set; }

        public Uri ArtistThumb { get; set; }

        public Uri ArtistArt { get; set; }
    }
}
