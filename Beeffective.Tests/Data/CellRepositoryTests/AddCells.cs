using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class AddCells : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1, CellEntity2, CellEntity3);
        }

        [Test]
        public async Task LoadAsync_Contain_AddedCells()
        {
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().NotBeNull();
            cellEntities.Should().Contain(CellEntity1);
            cellEntities.Should().Contain(CellEntity2);
            cellEntities.Should().Contain(CellEntity3);
        }
    }
}