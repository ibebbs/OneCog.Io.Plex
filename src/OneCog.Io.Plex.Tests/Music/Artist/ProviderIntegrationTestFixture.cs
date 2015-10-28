using NUnit.Framework;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Music.Artist
{
    [TestFixture(Category = "Integration")]
    public class ProviderIntegrationTestFixture
    {
        [Test]
        public async Task ShouldBeAbleToRetrieveAllArtists()
        {
            Plex.Api.Instance api = new Plex.Api.Instance("winplex", 32400);
            Plex.Section.Provider sectionProvider = new Plex.Section.Provider(api);

            Plex.Music.Artist.Provider provider = new Plex.Music.Artist.Provider(
                sectionProvider, api
            );

            IList<Plex.Music.IArtist> artists = await provider.All.ToList();

            Assert.That(artists, Is.Not.Null);
            Assert.That(artists.Count, Is.GreaterThan(0));
        }
    }
}
