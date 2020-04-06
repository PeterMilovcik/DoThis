using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class Width : TestFixture
    {
        private const int NewWidth = 4999;

        public override void SetUp()
        {
            base.SetUp();
            Sut.Width = NewWidth;
        }

        [Test]
        public void IsSet() =>
            Sut.Width.Should().Be(NewWidth);

        [Test]
        public void PropertyChanged_Event_IsRaised() => 
            ChangedPropertyName.Should().Be(nameof(Sut.Width));
    }
}