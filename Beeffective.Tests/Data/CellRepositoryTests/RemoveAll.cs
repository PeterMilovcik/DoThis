using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class RemoveAll : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(new[] {CellEntity1, CellEntity2, CellEntity3});
            Sut.RemoveAll();
        }

        [Test]
        public async Task LoadAsync_Empty()
        {
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().BeEmpty();
        }
    }
}
