using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Music.Album
{
    [TestFixture(Category = "Integration")]
    public class ProviderIntegrationTestFixture
    {
        [Test]
        public async Task ShouldBeAbleToAllAlbums()
        {
            Plex.Api.Instance api = new Plex.Api.Instance(new Uri("http://winplex:32400"));
            Plex.Section.Provider sectionProvider = new Plex.Section.Provider(api);

            Plex.Music.Album.Provider provider = new Plex.Music.Album.Provider(sectionProvider, api);

            IList<Plex.Music.IAlbum> artists = await provider.All.ToList();

            Assert.That(artists, Is.Not.Null);
            Assert.That(artists.Count, Is.GreaterThan(0));
        }

        [Test]
        [TestCase("/library/metadata/60734/children")]
        public async Task ShouldBeAbleToRetrieveAlbumsForArtist(string artistMetadataKey)
        {
            Plex.Api.Instance api = new Plex.Api.Instance(new Uri("http://winplex:32400"));
            Plex.Section.Provider sectionProvider = new Plex.Section.Provider(api);

            Plex.Music.Album.Provider provider = new Plex.Music.Album.Provider(sectionProvider, api);

            Plex.Music.IArtist artist = A.Fake<Plex.Music.IArtist>();
            A.CallTo(() => artist.Key).Returns(artistMetadataKey);

            IList<Plex.Music.IAlbum> artists = await provider.ForArtist(artist).ToList();

            Assert.That(artists, Is.Not.Null);
            Assert.That(artists.Count, Is.GreaterThan(0));
        }
    }
}
