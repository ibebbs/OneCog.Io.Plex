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
        private readonly Section.IProvider _sections;
        private readonly Api.IInstance _api;

        public Provider(Section.IProvider sections, Api.IInstance api)
        {
            _sections = sections;
            _api = api;
        }

        private IAlbum Map(Api.Directory directory)
        {
            return new Instance
            {
                Key = directory.Key,
                Title = directory.Title,
                Summary = directory.Summary,
                Studio = directory.Studio,
                ArtistKey = directory.ParentKey,
                Year = directory.Year,
                Art = new Uri(_api.Host, directory.ArtUri),
                Thumb = new Uri(_api.Host, directory.ThumbnailUri)
            };
        }

        public IObservable<IAlbum> ForArtist(IArtist artist)
        {
            return _api.GetAlbumsForArtist(artist.Key).ToObservable()
                .SelectMany(results => results)
                .Select(Map);
        }

        public IObservable<IAlbum> All 
        { 
            get
            {
                return _sections.All
                    .OfType<Section.IMusic>()
                    .SelectMany(section => _api.GetAllAlbums(section.Key))
                    .SelectMany(results => results)
                    .Select(Map);
            }
        }
    }
}
