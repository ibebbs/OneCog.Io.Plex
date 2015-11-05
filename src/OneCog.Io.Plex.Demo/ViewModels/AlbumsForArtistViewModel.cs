using Caliburn.Micro;
using Reactive.EventAggregator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using IEventAggregator = Reactive.EventAggregator.IEventAggregator;

namespace OneCog.Io.Plex.Demo.ViewModels
{
    public interface IAlbumsForArtistViewModel : IScreen
    {
        IEnumerable<Music.IAlbum> Albums { get; }
    }

    internal class AlbumsForArtistViewModel : Screen, IAlbumsForArtistViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<Music.IAlbum> _albums;

        private readonly IDisposable _behaviors;

        public AlbumsForArtistViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _albums = new ObservableCollection<Music.IAlbum>();

            _behaviors = new CompositeDisposable(
                EnsureAlbumsAreRetrievedWhenServerAvailableAndArtistSelected()
            );
        }

        private IDisposable EnsureAlbumsAreRetrievedWhenServerAvailableAndArtistSelected()
        {
            IObservable<ICollectionAction> albumActions = Observable.CombineLatest(
                _eventAggregator.GetEvent<Message.ServerConnectionAvailable>().Select(message => message.Server),
                _eventAggregator.GetEvent<Message.ArtistSelected>().Select(message => message.Artist),
                (server, artist) => new { Server = server, Artist = artist })
                .Select(tuple => tuple.Server.Music.Albums.ForArtist(tuple.Artist)
                    .Select<Music.IAlbum, ICollectionAction>(album => Collection<Music.IAlbum>.Add(album))
                    .StartWith(Collection<Music.IAlbum>.Clear()))
                .Switch()
                .ObserveOnDispatcher()
                .Publish()
                .RefCount();

            return new CompositeDisposable(
                albumActions.OfType<Clear>().Subscribe(_ => _albums.Clear()),
                albumActions.OfType<Add<Music.IAlbum>>().Subscribe(action => _albums.Add(action.Item))
            );
        }

        public IEnumerable<Music.IAlbum> Albums
        {
            get { return _albums; }
        }
    }
}
