using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Music.Album
{
    [TestFixture(Category = "Integration")]
    public class ProviderIntegrationTestFixture
    {
        [Test]
        [TestCase("/library/metadata/60734/children")]
        public async Task ShouldBeAbleToRetrieveAlbumsForArtist(string artistMetadataKey)
        {
            Plex.Api.Instance api = new Plex.Api.Instance("winplex", 32400);

            Plex.Music.Album.Provider provider = new Plex.Music.Album.Provider(api);

            Plex.Music.Models.IArtist artist = A.Fake<Plex.Music.Models.IArtist>();
            A.CallTo(() => artist.Key).Returns(artistMetadataKey);

            IList<Plex.Music.Models.IAlbum> artists = await provider.ForArtist(artist).ToList();

            Assert.That(artists, Is.Not.Null);
            Assert.That(artists.Count, Is.GreaterThan(0));
        }
    }
}
