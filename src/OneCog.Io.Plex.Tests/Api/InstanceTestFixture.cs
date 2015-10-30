using NUnit.Framework;
using OneCog.Io.Plex.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Tests.Api
{
    [TestFixture(Category = "Integration")]
    public class InstanceTestFixture
    {
        [Test]
        public async Task ShouldBeAbleToGetSections()
        {
            Instance instance = new Instance(new Uri("http://winplex:32400"));

            IEnumerable<Directory> sections = await instance.GetSections();

            Assert.That(sections, Is.Not.Null);
            Assert.That(sections.Count(), Is.EqualTo(4));
        }
    }
}
