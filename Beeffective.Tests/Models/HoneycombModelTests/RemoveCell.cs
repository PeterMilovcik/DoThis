using Beeffective.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class RemoveCell : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CellModel1 = Sut.AddCell(CellModel1);
            Sut.RemoveCell(CellModel1);
        }

        [Test]
        public void Cells_DoesNotContain_AddedCellModel() => 
            Sut.Cells.Should().NotContain(CellModel1);
    }
}
