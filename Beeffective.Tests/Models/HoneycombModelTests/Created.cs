using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class Created : TestFixture
    {
        [Test] 
        public void Cells_NotNull() => Sut.Cells.Should().NotBeNull();
    }
}