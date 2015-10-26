using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Section
{
    public interface IProvider
    {
        IObservable<ISection> All { get; }
    }

    internal class Provider : IProvider
    {
        static Provider()
        {
            AutoMapper.Mapper.Configuration
                .CreateMap<Api.Directory, ISection>()
                .ConvertUsing<DirectoryToSectionConverter>();

            AutoMapper.Mapper.Configuration
                .CreateMap<Api.Directory, Show>();
            AutoMapper.Mapper.Configuration.CreateMap<Api.Directory, Music>();
            AutoMapper.Mapper.Configuration.CreateMap<Api.Directory, Movie>();
        }

        public Provider(Api.IInstance api)
        {
            All = api.GetSections().ToObservable()
                .SelectMany(src => src.Select(directory => AutoMapper.Mapper.Map<ISection>(directory)));
        }

        public IObservable<ISection> All { get; private set; }
    }
}
