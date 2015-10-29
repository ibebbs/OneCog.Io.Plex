using System.Xml.Serialization;

namespace OneCog.Io.Plex.Api
{
    [XmlRoot("Track")]
    public class Track
    {
        [XmlAttribute("key")] 
        public string Key { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("summary")]
        public string Summary { get; set; }

        [XmlAttribute("year")]
        public int Year { get; set; }

        [XmlAttribute("duration")]
        public int Duration { get; set; }

        [XmlAttribute("thumb")]
        public string ThumbUri { get; set; }

        [XmlAttribute("art")]
        public string ArtUri { get; set; }

        [XmlAttribute("parentKey")]
        public string AlbumKey { get; set; }

        [XmlAttribute("parentTitle")]
        public string AlbumTitle { get; set; }

        [XmlAttribute("parentThumb")]
        public string AlbumThumb { get; set; }

        [XmlAttribute("grandparentKey")]
        public string ArtistKey { get; set; }

        [XmlAttribute("grandparentTitle")]
        public string ArtistName { get; set; }

        [XmlAttribute("grandparentThumb")]
        public string ArtistThumbUri { get; set; }

        [XmlAttribute("grandparentArt")]
        public string ArtistArtUri { get; set; }
    }
}
