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
        public void EmptyCells_NotNull() =>
            Sut.EmptyCells.Should().NotBeNull();

        [Test]
        public void FullCells_NotNull() =>
            Sut.FullCells.Should().NotBeNull();
    }
}
