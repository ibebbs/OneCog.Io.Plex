using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    [XmlRoot("Country")]
    public class Country
    {
        [XmlAttribute("tag")]
        public string Name { get; set; }
    }
}