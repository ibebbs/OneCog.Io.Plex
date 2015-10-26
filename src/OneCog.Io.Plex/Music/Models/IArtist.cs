using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Models
{
    public interface IArtist
    {
        string Key { get; }

        string Name { get; }

        string Summary { get; }

        string ArtUri { get; }

        string ThumbUri { get; }
    }
}
