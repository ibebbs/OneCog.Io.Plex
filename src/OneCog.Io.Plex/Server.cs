
using System;
namespace OneCog.Io.Plex
{
    public interface IServer
    {
        Section.IProvider Sections { get; }

        Music.IProvider Music { get; }

        Transcoder.IProvider Transcoder { get; }
    }

    public class Server : IServer
    {
        public static IServer Create(Uri host)
        {
            Api.IInstance api = new Api.Instance(host);
            Section.IProvider sections = new Section.Provider(api);
            Music.IProvider music = new Music.Provider(
                new Music.Artist.Provider(sections, api),
                new Music.Album.Provider(sections, api),
                new Music.Track.Provider(api)
            );
            Transcoder.IProvider transcoder = new Transcoder.Provider(api);

            return new Server(sections, music, transcoder);
        }

        public static IServer Create(string hostName, ushort port)
        {
            UriBuilder builder = new UriBuilder();
            builder.Scheme = "http";
            builder.Host = hostName;
            builder.Port = port;

            return Create(builder.Uri);
        }

        private Server(Section.IProvider sections, Music.IProvider music, Transcoder.IProvider transcoder)
        {
            Sections = sections;
            Music = music;
            Transcoder = transcoder;
        }

        public Section.IProvider Sections { get; private set; }

        public Music.IProvider Music { get; private set; }

        public Transcoder.IProvider Transcoder { get; private set; }
    }
}
