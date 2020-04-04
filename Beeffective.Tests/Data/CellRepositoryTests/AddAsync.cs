using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class AddAsync : TestFixture
    {
        [Test]
        public async Task Contains_AddedCell()
        {
            await Sut.AddAsync(CellEntity1);
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().Contain(CellEntity1);
        }
    }
}