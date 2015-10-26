using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Section
{
    public interface ISection
    {
        Guid Id { get; }

        string Key { get; }
    }
}
