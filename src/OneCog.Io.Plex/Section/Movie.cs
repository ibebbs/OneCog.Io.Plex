using System;

namespace OneCog.Io.Plex.Section
{
    public interface IMovie : ISection
    {

    }

    internal class Movie : IMovie
    {
        public Guid Id { get; private set; }

        public string Key { get; private set; }
    }
}
