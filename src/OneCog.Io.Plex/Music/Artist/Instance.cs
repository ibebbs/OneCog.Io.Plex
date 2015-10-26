using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Artist
{
    internal class Instance : Models.IArtist
    {
        public string Key { get; private set; }

        public string Name { get; private set; }

        public string Summary { get; private set; }

        public string ArtUri { get; private set; }

        public string ThumbUri { get; private set; }
    }
}
