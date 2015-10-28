using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Album
{
    public interface IProvider
    {
        IObservable<IAlbum> ForArtist(IArtist artist);

        IObservable<IAlbum> All { get; }
    }

    internal class Provider : IProvider
    {
        private readonly Api.IInstance _api;

        static Provider()
        {
            AutoMapper.Mapper.CreateMap<Api.Directory, Instance>()
                .ForMember(instance => instance.ArtistKey, map => map.MapFrom(directory => directory.ParentKey));
        }

        public Provider(Section.IProvider sections, Api.IInstance api)
        {
            _api = api;

            All = sections.All.OfType<Section.IMusic>()
                .SelectMany(section => api.GetAllAlbums(section.Key))
                .Select(AutoMapper.Mapper.Map<Instance>);
        }

        public IObservable<IAlbum> ForArtist(IArtist artist)
        {
            return _api.GetAlbumsForArtist(artist.Key).ToObservable()
                .Select(AutoMapper.Mapper.Map<Instance>);
        }

        public IObservable<IAlbum> All { get; private set; }
    }
}
