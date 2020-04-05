using System.Linq;
using Beeffective.Data;
using Beeffective.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class CellModelChanged : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CellModel1 = Sut.AddCell(CellModel1);
        }

        [Test]
        public void TitleChanged_CellModelIsUpdated()
        {
            const string newTitle = "test cell model 1 changed";
            CellModel1.Title = newTitle;
            Repository = new CellRepository();
            Sut = new HoneycombModel(Repository);
            Sut.Load();
            var changedCellModel = Sut.Cells.FirstOrDefault(
                cellModel => cellModel.Title == newTitle);
            changedCellModel.Should().NotBeNull();
        }
    }
}
