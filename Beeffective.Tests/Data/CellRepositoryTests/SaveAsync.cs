using System.Threading.Tasks;
using Beeffective.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class SaveAsync : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1, CellEntity2, CellEntity3);
        }

        [Test]
        public async Task SaveAsync_Is_Persistent()
        {
            Sut = new CellRepository();
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().Contain(CellEntity1);
            cellEntities.Should().Contain(CellEntity2);
            cellEntities.Should().Contain(CellEntity3);
        }
    }
}
