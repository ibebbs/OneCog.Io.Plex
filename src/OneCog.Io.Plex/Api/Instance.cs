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

        Task<IEnumerable<Directory>> GetArtists(string key);

        Task<IEnumerable<Directory>> GetAlbums(string artistMetadataKey);
    }

    internal class Instance : IInstance
    {
        private static readonly Serializer Serializer = new Serializer();

        private readonly string _host;
        private readonly ushort _port;

        public Instance(string host, ushort port)
        {
            _host = host;
            _port = port;
        }

        private HttpWebRequest ConstructRequest(string uri)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(string.Format("http://{0}:{1}/", _host, _port));
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

        public async Task<IEnumerable<Directory>> GetArtists(string key)
        {
            HttpWebRequest request = ConstructRequest(string.Format("library/sections/{0}/all", key));
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }

        public async Task<IEnumerable<Directory>> GetAlbums(string artistMetadataKey)
        {
            HttpWebRequest request = ConstructRequest(artistMetadataKey);
            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                MediaContainer container = Serializer.DeserializeMediaContainer(stream);

                return container.Directories.ToArray();
            }
        }
    }
}
