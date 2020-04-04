using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class Add : TestFixture
    {
        [Test]
        public async Task Contains_AddedCell()
        {
            Sut.Add(CellEntity1);
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().Contain(CellEntity1);
        }
    }
}