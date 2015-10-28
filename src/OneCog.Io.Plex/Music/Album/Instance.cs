
namespace OneCog.Io.Plex.Music.Album
{
    internal class Instance : IAlbum
    {
        public string Key { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string Studio { get; private set; }

        public string ArtistKey { get; private set; }

        public int Year { get; private set; }

        public string ArtUri { get; private set; }

        public string ThumbUri { get; private set; }
    }
}
