using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    [XmlRoot("MediaContainer")]
    public class MediaContainer
    {
        [XmlAttribute("title1")]
        public string Title { get; set; }

        [XmlAttribute("size")]
        public int Size { get; set; }

        [XmlAttribute("allowSync")]
        public bool AllowSync { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("mediaTagPrefix")]
        public string MediaTagPrefix { get; set; }

        [XmlAttribute("mediaTagVersion")]
        public long MediaTagVersion { get; set; }

        [XmlElement("Directory", typeof(Directory))]
        public Directory[] Directories { get; set; }
    }
}
