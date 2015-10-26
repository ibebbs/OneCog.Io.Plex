using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    
    public class Serializer
    {
        private static readonly XmlSerializer MediaContainerSerializer = new XmlSerializer(typeof(MediaContainer));

        public MediaContainer DeserializeMediaContainer(Stream stream)
        {
            return (MediaContainerSerializer.Deserialize(stream)) as MediaContainer;
        }
    }
}
