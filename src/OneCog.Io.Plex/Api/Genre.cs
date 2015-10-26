using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    [XmlRoot("Genre")]
    public class Genre
    {
        [XmlAttribute("tag")]
        public string Name { get; set; }
    }
}