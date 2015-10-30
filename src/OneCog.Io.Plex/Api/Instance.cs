using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Api
{
    public interface IInstance
    {
        Task<IEnumerable<Directory>> GetSections();

        Task<IEnumerable<Directory>> GetAllArtists(string sectionKey);

        Task<IEnumerable<Directory>> GetAllAlbums(string sectionKey);

        Task<IEnumerable<Directory>> GetAlbumsForArtist(string artistMetadataKey);

        Task<IEnumerable<Track>> GetTracksForAlbum(string albumMetadataKey);

        Uri Host { get; }
    }

    internal class Instance : IInstance
    {
        private static readonly Serializer Serializer = new Serializer();

        public Instance(Uri host)
        {
            if (host == null) throw new ArgumentNullException("host");
            if (!host.IsAbsoluteUri) throw new ArgumentException("Host uri must not be relative");

            Host = host;
        }

        private HttpWebRequest ConstructRequest(string uri)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(Host, uri));
            request.Method = "GET";

            return request;
        }

        public async Task<IEnumerable<Directory>> GetSections()
        {
            HttpWebRequest request = ConstructRequest("library/sections/all");
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }

        public async Task<IEnumerable<Directory>> GetAllArtists(string sectionKey)
        {
            HttpWebRequest request = ConstructRequest(string.Format("library/sections/{0}/all", sectionKey));
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }

        public async Task<IEnumerable<Directory>> GetAllAlbums(string sectionKey)
        {
            HttpWebRequest request = ConstructRequest(string.Format("library/sections/{0}/albums", sectionKey));
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }

        public async Task<IEnumerable<Directory>> GetAlbumsForArtist(string artistMetadataKey)
        {
            HttpWebRequest request = ConstructRequest(artistMetadataKey);
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }

        public async Task<IEnumerable<Track>> GetTracksForAlbum(string albumMetadataKey)
        {
            HttpWebRequest request = ConstructRequest(albumMetadataKey);
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Tracks.ToArray();
            }
        }

        public Uri Host { get; private set; }
    }
}
