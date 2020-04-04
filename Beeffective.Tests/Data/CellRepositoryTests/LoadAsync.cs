using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class LoadAsync : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1);
        }

        [Test]
        public async Task AddedCellEntity_Is_Loaded()
        {
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().Contain(CellEntity1);
        }
    }
}