using System.Linq;
using Beeffective.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class AddCell : TestFixture
    {
        private CellModel newCellModel;

        public override void Setup()
        {
            base.Setup();
            newCellModel = new CellModel();
            Sut.AddCell(newCellModel);
        }

        [Test]
        public void NewCellModel_Is_Added_To_Cells() => 
            Sut.Cells.Should().Contain(newCellModel);
    }
}
