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

        public Provider(Api.IInstance api)
        {
            _api = api;
        }

        private ITrack Map(Api.Track track)
        {
            return new Instance
            {
                Key = track.Key,
                Title = track.Title,
                Summary = track.Summary,
                Year = track.Year,
                Duration = track.Duration,
                Thumb = new Uri(_api.Host, track.ThumbUri),
                Art = new Uri(_api.Host, track.ArtUri),
                AlbumKey = track.AlbumKey,
                AlbumTitle = track.AlbumTitle,
                AlbumThumb = new Uri(_api.Host, track.AlbumThumb),
                ArtistKey = track.ArtistKey,
                ArtistName = track.ArtistName,
                ArtistThumb = new Uri(_api.Host, track.ArtistThumbUri),
                ArtistArt = new Uri(_api.Host, track.ArtistArtUri)
            };
        }

        public IObservable<ITrack> ForAlbum(IAlbum album)
        {
            return _api.GetTracksForAlbum(album.Key).ToObservable()
                .SelectMany(results => results)
                .Select(Map);
        }
    }
}
