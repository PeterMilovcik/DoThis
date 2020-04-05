using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.Entities.CellEntityTests
{
    class Title : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Title = Title;
        }

        [Test]
        public void IsSet() => 
            Sut.Title.Should().Be(Title);
    }
}