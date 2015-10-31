using System;

namespace OneCog.Io.Plex.Music
{
    public interface IArtist
    {
        string Key { get; }

        string Name { get; }

        string Summary { get; }

        Uri Art { get; }

        Uri Thumb { get; }
    }
}
