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

        Uri Thumb { get; set; }

        Uri Art { get; set; }

        string AlbumKey { get; set; }

        string AlbumTitle { get; set; }

        Uri AlbumThumb { get; set; }

        string ArtistKey { get; set; }

        string ArtistName { get; set; }

        Uri ArtistThumb { get; set; }

        Uri ArtistArt { get; set; }
    }
}
