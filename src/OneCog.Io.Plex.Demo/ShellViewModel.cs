using Caliburn.Micro;
using Caliburn.Micro.Reactive.Extensions;
using System;
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

        private IDisposable _behaviors;

        public ShellViewModel()
        {
            _host = new ObservableProperty<string>("winplex", this, () => Host);
            _port = new ObservableProperty<ushort>(32400, this, () => Port);
            _connectCommand = new ObservableCommand();
            _server = new Subject<IServer>();

            _behaviors = new CompositeDisposable(
                EnsureConnectCommandConnectsToServer()
            );
        }

        private IDisposable EnsureConnectCommandConnectsToServer()
        {
            return _connectCommand
                .SelectMany(Observable.CombineLatest(_host, _port, (host, port) => new Uri(string.Format("http://{0}:{1}", host, port))).Take(1))
                .Select(Server.Create)
                .Subscribe(_server);
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
    }
}