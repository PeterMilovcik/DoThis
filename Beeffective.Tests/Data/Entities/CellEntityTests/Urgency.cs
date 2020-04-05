using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.Entities.CellEntityTests
{
    class Urgency : TestFixture
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            Sut.Urgency = Urgency;
        }

        [Test]
        public void IsSet() =>
            Sut.Urgency.Should().Be(Urgency);
    }
}