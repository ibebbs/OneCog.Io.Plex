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
using IEventAggregator = Reactive.EventAggregator.IEventAggregator;

namespace OneCog.Io.Plex.Demo 
{
    public interface IShellViewModel  : IScreen
    {
    }

    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly ObservableProperty<string> _host;
        private readonly ObservableProperty<ushort> _port;
        private readonly ObservableCommand _connectCommand;

        private readonly Subject<IServer> _server;

        private IDisposable _behaviors;

        public ShellViewModel(ViewModels.IAllArtistsViewModel allArtistsViewModel, ViewModels.IAlbumsForArtistViewModel albumsForArtistViewModel, IEventAggregator eventAggregator)
        {
            Items.AddRange(new IScreen[] { allArtistsViewModel, albumsForArtistViewModel });
            
            AllArtistsViewModel = allArtistsViewModel;
            AlbumsForArtistViewModel = albumsForArtistViewModel;

            _eventAggregator = eventAggregator;

            _host = new ObservableProperty<string>("winplex", this, () => Host);
            _port = new ObservableProperty<ushort>(32400, this, () => Port);
            _connectCommand = new ObservableCommand();
            _server = new Subject<IServer>();

            ActivateItem(allArtistsViewModel);

            _behaviors = new CompositeDisposable(
                EnsureConnectCommandConnectsToServer(),
                EnsureServerConnectionIsPublished(),
                EnsureAlbumsForArtistsDisplayedWhenArtistSelected()
            );
        }

        private IDisposable EnsureAlbumsForArtistsDisplayedWhenArtistSelected()
        {
            return _eventAggregator
                .GetEvent<Message.ArtistSelected>()
                .Subscribe(_ => ActivateItem(AlbumsForArtistViewModel));
        }

        private IDisposable EnsureConnectCommandConnectsToServer()
        {
            return _connectCommand
                .SelectMany(Observable.CombineLatest(_host, _port, (host, port) => new Uri(string.Format("http://{0}:{1}", host, port))).Take(1))
                .Select(Server.Create)
                .Subscribe(_server);
        }

        private IDisposable EnsureServerConnectionIsPublished()
        {
            return _server.Subscribe(server => _eventAggregator.Publish(new Message.ServerConnectionAvailable(server)));
        }

        public ViewModels.IAllArtistsViewModel AllArtistsViewModel { get; private set; }

        public ViewModels.IAlbumsForArtistViewModel AlbumsForArtistViewModel { get; private set; }

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
    }
}