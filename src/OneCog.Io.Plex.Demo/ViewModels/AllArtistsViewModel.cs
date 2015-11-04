using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Demo.ViewModels
{
    public interface IAllArtistsViewModel : IScreen
    {
        IObserver<IServer> Server { get; }

        IEnumerable<Music.IArtist> Artists { get; }
    }

    public class AllArtistsViewModel : Screen, IAllArtistsViewModel
    {
        private readonly Subject<IServer> _server;
        private readonly ObservableCollection<Music.IArtist> _artists;

        private IDisposable _behaviours;

        public AllArtistsViewModel()
        {
            _server = new Subject<IServer>();
            _artists = new ObservableCollection<Music.IArtist>();

            _behaviours = new CompositeDisposable(
                EnsureArtistsAreRetrievedWhenServerIsConnected()
            );
        }

        private IDisposable EnsureArtistsAreRetrievedWhenServerIsConnected()
        {
            IObservable<ICollectionAction> artistActions = _server
                .Select(server =>
                    server.Music.Artists.All
                        .Select<Music.IArtist, ICollectionAction>(artist => Collection<Music.IArtist>.Add(artist))
                        .StartWith(Collection<Music.IArtist>.Clear()))
                .Switch()
                .ObserveOnDispatcher()
                .Publish()
                .RefCount();

            return new CompositeDisposable(
                artistActions.OfType<Clear>().Subscribe(_ => _artists.Clear()),
                artistActions.OfType<Add<Music.IArtist>>().Subscribe(action => _artists.Add(action.Item))
            );
        }

        public IObserver<IServer> Server
        {
            get { return _server; }
        }

        public IEnumerable<Music.IArtist> Artists
        {
            get { return _artists; }
        }
    }
}
