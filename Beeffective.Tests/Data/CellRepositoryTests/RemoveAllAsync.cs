using System.Threading.Tasks;
using Beeffective.Data.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class RemoveAllAsync : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1, CellEntity2, CellEntity3);
        }

        [Test]
        public async Task LoadAsync_ReturnsEmptyList()
        {
            await Sut.RemoveAllAsync();
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().BeEmpty();
        }
    }
}