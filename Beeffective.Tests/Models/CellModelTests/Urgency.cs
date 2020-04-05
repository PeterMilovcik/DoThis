using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.CellModelTests
{
    class Urgency : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Urgency = 100;
        }

        [Test]
        public void Urgency_IsSet() =>
            Sut.Urgency.Should().Be(100);

        [Test]
        public void PropertyChanged_Event_IsRaised() =>
            ChangedPropertyName.Should().Be(nameof(Sut.Urgency));
    }
}
