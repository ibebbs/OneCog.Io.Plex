using System;

namespace OneCog.Io.Plex.Section
{
    public interface IShow : ISection
    {

    }

    internal class Show : IShow
    {
        public Guid Id { get; private set; }

        public string Key { get; private set; }
    }
}
