using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCog.Io.Plex
{
    public class Server
    {
        public Server(Section.IProvider sections, Music.IProvider music)
        {
            Sections = sections;
            Music = music;
        }

        public Section.IProvider Sections { get; private set; }

        public Music.IProvider Music { get; private set; }
    }
}
