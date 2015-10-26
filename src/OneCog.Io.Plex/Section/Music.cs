using System;

namespace OneCog.Io.Plex.Section
{
    public interface IMusic : ISection
    {

    }

    internal class Music : IMusic
    {
        public Guid Id { get; private set; }

        public string Key { get; private set; }
    }
}
