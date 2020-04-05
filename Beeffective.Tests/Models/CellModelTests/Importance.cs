using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.CellModelTests
{
    class Importance : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Importance = 100;
        }

        [Test]
        public void Importance_IsSet() => 
            Sut.Importance.Should().Be(100);

        [Test]
        public void PropertyChanged_Event_IsRaised() => 
            ChangedPropertyName.Should().Be(nameof(Sut.Importance));
    }
}
