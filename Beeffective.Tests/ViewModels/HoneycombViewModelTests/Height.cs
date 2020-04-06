using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class Height : TestFixture
    {
        private const int NewHeight = 4998;

        public override void SetUp()
        {
            base.SetUp();
            Sut.Height = NewHeight;
        }

        [Test]
        public void IsSet() => 
            Sut.Height.Should().Be(NewHeight);

        [Test]
        public void PropertyChanged_Event_IsRaised() =>
            ChangedPropertyName.Should().Be(nameof(Sut.Height));
    }
}