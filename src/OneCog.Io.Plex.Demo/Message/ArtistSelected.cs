
namespace OneCog.Io.Plex.Demo.Message
{
    public class ArtistSelected
    {
        public ArtistSelected(Music.IArtist artist)
        {
            Artist = artist;
        }

        public Music.IArtist Artist { get; private set; }
    }
}
