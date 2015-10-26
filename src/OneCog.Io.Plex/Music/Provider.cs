using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music
{
    public interface IProvider
    {
        Artist.IProvider Artists { get; }
    }

    internal class Provider : IProvider
    {
        public Provider(Artist.IProvider artistProvider)
        {
            Artists = artistProvider;
        }

        public Artist.IProvider Artists { get; }
    }
}
