using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Section
{
    [TestFixture(Category = "Integration")]
    public class ProviderTestFixture
    {
        [Test]
        public async Task ShouldBeAbleToGetSections()
        {
            Plex.Section.Provider provider = new Plex.Section.Provider(
                new Plex.Api.Instance(new Uri("http://winplex:32400"))
            );

            IList<Plex.Section.ISection> sections = await provider.All.ToList();

            Assert.That(sections.Count, Is.EqualTo(4));
            Assert.That(sections.OfType<Plex.Section.IMusic>().Count(), Is.EqualTo(1));
            Assert.That(sections.OfType<Plex.Section.IMusic>().Single().Key, Is.EqualTo("5"));
            Assert.That(sections.OfType<Plex.Section.IShow>().Count(), Is.EqualTo(2));
            Assert.That(sections.OfType<Plex.Section.IMovie>().Count(), Is.EqualTo(1));
        }
    }
}
