using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music
{
    public interface IAlbum
    {
        string Key { get; }

        string Title { get; }

        string Summary { get; }

        string Studio { get; }

        string ArtistKey { get; }

        int Year { get; }

        string ArtUri { get; }

        string ThumbUri { get; }
    }
}
