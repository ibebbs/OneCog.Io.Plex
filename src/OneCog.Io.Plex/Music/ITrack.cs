using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music
{
    public interface ITrack
    {
        string Key { get; set; }

        string Title { get; set; }

        string Summary { get; set; }

        int Year { get; set; }

        int Duration { get; set; }

        string ThumbUri { get; set; }

        string ArtUri { get; set; }

        string AlbumKey { get; set; }

        string AlbumTitle { get; set; }

        string AlbumThumb { get; set; }

        string ArtistKey { get; set; }

        string ArtistName { get; set; }

        string ArtistThumbUri { get; set; }

        string ArtistArtUri { get; set; }
    }
}
