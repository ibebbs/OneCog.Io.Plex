using OneCog.Io.Plex.Api;
using System;

namespace OneCog.Io.Plex.Section
{
    internal class DirectoryToSectionConverter : AutoMapper.TypeConverter<Api.Directory, ISection>
    {
        protected override ISection ConvertCore(Directory source)
        {
            switch (source.Type.ToUpper())
            {
                case "SHOW": return AutoMapper.Mapper.Map<Show>(source);
                case "MOVIE": return AutoMapper.Mapper.Map<Movie>(source);
                case "ARTIST": return AutoMapper.Mapper.Map<Music>(source);
                default: throw new InvalidOperationException(string.Format("Unknown source type: '{0}'", source.Type));
            }
        }
    }
}
