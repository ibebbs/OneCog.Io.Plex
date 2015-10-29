
namespace OneCog.Io.Plex.Music
{
    public interface IProvider
    {
        Artist.IProvider Artists { get; }

        Album.IProvider Albums { get; }

        Track.IProvider Tracks { get; }
    }

    internal class Provider : IProvider
    {
        public Provider(Artist.IProvider artistProvider, Album.IProvider albumProvider, Track.IProvider trackProvider)
        {
            Artists = artistProvider;
            Albums = albumProvider;
            Tracks = trackProvider;
        }

        public Artist.IProvider Artists { get; private set; }

        public Album.IProvider Albums { get; private set; }

        public Track.IProvider Tracks { get; private set; }
    }
}
