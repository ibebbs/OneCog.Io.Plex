using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    [XmlRoot("Directory")]
    public class Directory
    {
        [XmlAttribute("uuid")]
        public Guid Id { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("parentKey")]
        public string ParentKey { get; set; }

        [XmlAttribute("parentTitle")]
        public string ParentTitle { get; set; }

        [XmlAttribute("parentThumb")]
        public string ParentThumb { get; set; }

        [XmlAttribute("summary")]
        public string Summary { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("year")]
        public int Year { get; set; }

        [XmlAttribute("art")]
        public string ArtUri { get; set; }

        [XmlAttribute("thumb")]
        public string ThumbnailUri { get; set; }

        [XmlAttribute("composite")]
        public string CompositeUri { get; set; }

        [XmlAttribute("studio")]
        public string Studio { get; set; }

        [XmlElement("Genre")]
        public Genre[] Genres { get; set; }

        [XmlElement("Country")]
        public Country Country { get; set; }
    }
}
