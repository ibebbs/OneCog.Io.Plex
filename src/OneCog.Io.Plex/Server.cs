
namespace OneCog.Io.Plex
{
    public interface IServer
    {
        Section.IProvider Sections { get; }

        Music.IProvider Music { get; }
    }

    public class Server : IServer
    {
        public static IServer Create(string hostName, ushort port)
        {
            Api.IInstance api = new Api.Instance(hostName, port);
            Section.IProvider sections = new Section.Provider(api);
            Music.IProvider music = new Music.Provider(
                new Music.Artist.Provider(sections, api),
                new Music.Album.Provider(sections, api)
            );

            return new Server(sections, music);
        }


        private Server(Section.IProvider sections, Music.IProvider music)
        {
            Sections = sections;
            Music = music;
        }

        public Section.IProvider Sections { get; private set; }

        public Music.IProvider Music { get; private set; }
    }
}
