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
        IObservable<IArtist> All { get; }
    }

    internal class Provider : IProvider
    {
        private readonly Section.IProvider _sectionProvider;
        private readonly Api.IInstance _api;

        public Provider(Section.IProvider sectionProvider, Api.IInstance api)
        {
            _sectionProvider = sectionProvider;
            _api = api;
        }

        private IArtist Map(Api.Directory directory)
        {
            return new Instance
            {
                Key = directory.Key,
                Name = directory.Title,
                Summary = directory.Summary,
                Art = new Uri(_api.Host, directory.ArtUri),
                Thumb = new Uri(_api.Host, directory.ThumbnailUri)
            };
        }

        public IObservable<IArtist> All 
        { 
            get
            {
                return _sectionProvider.All
                    .OfType<Section.IMusic>()
                    .SelectMany(section => _api.GetAllArtists(section.Key))
                    .SelectMany(results => results)
                    .Select(Map);
            }
        }
    }
}
