using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class Created : TestFixture
    {
        [Test]
        public void Width() => 
            Sut.Width.Should().Be(10000);

        [Test]
        public void Height() => 
            Sut.Height.Should().Be(10000);

        [Test]
        public void Cells_NotNull() => 
            Sut.Cells.Should().NotBeNull();
    }
}
