using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace OneCog.Io.Plex.Music.Track
{
    public interface IProvider
    {
        IObservable<ITrack> ForAlbum(IAlbum album);
    }

    internal class Provider : IProvider
    {
        private readonly Api.IInstance _api;

        static Provider()
        {
            AutoMapper.Mapper.CreateMap<Api.Track, Instance>();
        }

        public Provider(Api.IInstance api)
        {
            _api = api;
        }

        public IObservable<ITrack> ForAlbum(IAlbum album)
        {
            return _api.GetTracksForAlbum(album.Key).ToObservable()
                .Select(AutoMapper.Mapper.Map<Instance>);
        }
    }
}
