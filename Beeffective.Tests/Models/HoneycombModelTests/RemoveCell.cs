using Beeffective.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class RemoveCell : TestFixture
    {
        private CellModel cellModel;

        public override void Setup()
        {
            base.Setup();
            cellModel = new CellModel();
            Sut.AddCell(cellModel);
            Sut.RemoveCell(cellModel);
        }

        [Test]
        public void Cells_DoesNotContain_AddedCellModel() => 
            Sut.Cells.Should().NotContain(cellModel);
    }
}
