using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class Load : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CellModel1 = Sut.AddCell(CellModel1);
            Sut.Load();
        }

        [Test]
        public void Cells_Contain_AddedCellModel() => 
            Sut.Cells.Should().Contain(CellModel1);
    }
}
