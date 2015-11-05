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
using IEventAggregator = Reactive.EventAggregator.IEventAggregator;
using Caliburn.Micro.Reactive.Extensions;
using System.Windows.Input;

namespace OneCog.Io.Plex.Demo.ViewModels
{
    public interface IAllArtistsViewModel : IScreen
    {
        IEnumerable<Music.IArtist> Artists { get; }
    }

    public class AllArtistsViewModel : Screen, IAllArtistsViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<Music.IArtist> _artists;
        private readonly ObservableCommand _selectArtistCommand;

        private IDisposable _behaviours;

        public AllArtistsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _artists = new ObservableCollection<Music.IArtist>();
            _selectArtistCommand = new ObservableCommand();

            _behaviours = new CompositeDisposable(
                EnsureArtistsAreRetrievedWhenServerIsConnected(),
                EnsureArtistSelectedMessagePublishedWhenSelectArtistCommandExecuted()
            );
        }

        private IDisposable EnsureArtistSelectedMessagePublishedWhenSelectArtistCommandExecuted()
        {
            return _selectArtistCommand
                .OfType<Music.IArtist>()
                .Select(artist => new Message.ArtistSelected(artist))
                .Subscribe(_eventAggregator.Publish);
        }

        private IDisposable EnsureArtistsAreRetrievedWhenServerIsConnected()
        {
            IObservable<ICollectionAction> artistActions = _eventAggregator
                .GetEvent<Message.ServerConnectionAvailable>()
                .Select(message => message.Server)
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

        public IEnumerable<Music.IArtist> Artists
        {
            get { return _artists; }
        }

        public ICommand SelectArtistCommand
        {
            get { return _selectArtistCommand; }
        }
    }
}
