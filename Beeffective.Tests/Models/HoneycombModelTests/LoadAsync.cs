using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class LoadAsync : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CellModel1 = Sut.AddCell(CellModel1);
        }

        [Test]
        public async Task Contain_Loaded_CellModel()
        {
            await Sut.LoadAsync();
            Sut.Cells.Should().Contain(CellModel1);
        }
    }
}
