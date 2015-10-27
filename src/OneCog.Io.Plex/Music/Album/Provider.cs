using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Album
{
    public interface IProvider
    {
        IObservable<Models.IAlbum> ForArtist(Models.IArtist artist);
    }

    internal class Provider : IProvider
    {
        private readonly Api.IInstance _api;

        static Provider()
        {
            AutoMapper.Mapper.CreateMap<Api.Directory, Instance>()
                .ForMember(instance => instance.ArtistKey, map => map.MapFrom(directory => directory.ParentKey));
        }

        public Provider(Api.IInstance api)
        {
            _api = api;
        }

        public IObservable<Models.IAlbum> ForArtist(Models.IArtist artist)
        {
            return _api.GetAlbums(artist.Key).ToObservable()
                .Select(AutoMapper.Mapper.Map<Instance>);
        }
    }
}
