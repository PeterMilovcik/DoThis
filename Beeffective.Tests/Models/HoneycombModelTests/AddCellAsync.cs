using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class AddCellAsync : TestFixture
    {
        [Test]
        public async Task Cells_Contain_AddedCellModel()
        {
            var addedCellModel = await Sut.AddCellAsync(CellModel1);
            Sut.Cells.Should().Contain(addedCellModel);
        }
    }
}
