using Caliburn.Micro;
using Caliburn.Micro.Reactive.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;

namespace OneCog.Io.Plex.Demo 
{
    public interface IShellViewModel  : IScreen
    {
    }

    public class ShellViewModel : Screen, IShellViewModel 
    {
        private readonly ObservableProperty<string> _host;
        private readonly ObservableProperty<ushort> _port;
        private readonly ObservableCommand _connectCommand;

        private readonly Subject<IServer> _server;

        private readonly ObservableCollection<Music.IArtist> _artists;

        private IDisposable _behaviors;

        public ShellViewModel()
        {
            _host = new ObservableProperty<string>("winplex", this, () => Host);
            _port = new ObservableProperty<ushort>(32400, this, () => Port);
            _connectCommand = new ObservableCommand();
            _server = new Subject<IServer>();
            _artists = new ObservableCollection<Music.IArtist>();

            _behaviors = new CompositeDisposable(
                EnsureConnectCommandConnectsToServer(),
                EnsureArtistsAreRetrievedWhenServerIsConnected()
            );
        }

        private IDisposable EnsureConnectCommandConnectsToServer()
        {
            return _connectCommand
                .SelectMany(Observable.CombineLatest(_host, _port, (host, port) => new Uri(string.Format("http://{0}:{1}", host, port))).Take(1))
                .Select(Server.Create)
                .Subscribe(_server);
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

        public string Host 
        {
            get { return _host.Get(); }
            set { _host.Set(value); }
        }

        public ushort Port 
        {
            get { return _port.Get(); }
            set { _port.Set(value); }
        }

        public ICommand ConnectCommand
        {
            get { return _connectCommand; }
        }

        public IEnumerable<Music.IArtist> Artists
        {
            get { return _artists; }
        }
    }
}