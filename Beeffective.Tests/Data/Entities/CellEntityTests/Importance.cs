using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.Entities.CellEntityTests
{
    class Importance : TestFixture
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            Sut.Importance = Importance;
        }

        [Test]
        public void IsSet() => 
            Sut.Importance.Should().Be(Importance);
    }
}