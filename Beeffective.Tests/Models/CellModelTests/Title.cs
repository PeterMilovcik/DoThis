using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.CellModelTests
{
    class Title : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Title = Title;
        }

        [Test]
        public void Title_IsSet() => Sut.Title.Should().Be(Title);

        [Test]
        public void PropertyChanged_Event_IsRaised() => 
            ChangedPropertyName.Should().Be(nameof(Sut.Title));
    }
}