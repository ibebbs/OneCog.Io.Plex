using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Artist
{
    public interface IProvider
    {
        IObservable<Models.IArtist> All { get; }
    }

    internal class Provider : IProvider
    {
        static Provider()
        {
            AutoMapper.Mapper.Configuration
                .CreateMap<Api.Directory, Instance>()
                .ForMember(instance => instance.Name, mapping => mapping.MapFrom(directory => directory.Title));
        }

        public Provider(Section.IProvider sectionProvider, Api.IInstance api)
        {
            All = sectionProvider.All
                .OfType<Section.IMusic>()
                .SelectMany(section => api.GetAllArtists(section.Key))
                .Select(AutoMapper.Mapper.Map<Instance>);
        }

        public IObservable<Models.IArtist> All { get; private set; }
    }
}
