using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCog.Io.Plex
{
    public class Server
    {
        public Section.IProvider Sections { get; }

        public Music.IProvider Music { get; }
    }
}
