using System;

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

        Uri Art { get; }

        Uri Thumb { get; }
    }
}
