
namespace OneCog.Io.Plex.Music
{
    public interface IProvider
    {
        Artist.IProvider Artists { get; }

        Album.IProvider Albums { get; }
    }

    internal class Provider : IProvider
    {
        public Provider(Artist.IProvider artistProvider, Album.IProvider albumProvider)
        {
            Artists = artistProvider;
            Albums = albumProvider;
        }

        public Artist.IProvider Artists { get; private set; }

        public Album.IProvider Albums { get; private set; }
    }
}
