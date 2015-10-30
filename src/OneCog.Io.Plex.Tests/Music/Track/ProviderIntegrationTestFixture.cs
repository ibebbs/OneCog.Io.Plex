using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Music.Track
{
    [TestFixture(Category = "Integration")]
    public class ProviderIntegrationTestFixture
    {
        [Test]
        [TestCase("/library/metadata/60734/children")]
        public async Task ShouldBeAbleToRetrieveTracksForAlbum(string albumMetadataKey)
        {
            Plex.Api.Instance api = new Plex.Api.Instance(new Uri("http://winplex:32400"));

            Plex.Music.Track.Provider provider = new Plex.Music.Track.Provider(api);

            Plex.Music.IAlbum artist = A.Fake<Plex.Music.IAlbum>();
            A.CallTo(() => artist.Key).Returns(albumMetadataKey);

            IList<Plex.Music.ITrack> artists = await provider.ForAlbum(artist).ToList();

            Assert.That(artists, Is.Not.Null);
            Assert.That(artists.Count, Is.GreaterThan(0));
        }
    }
}
