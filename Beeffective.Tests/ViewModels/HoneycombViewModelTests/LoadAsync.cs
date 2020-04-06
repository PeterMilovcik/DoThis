using System.Linq;
using System.Threading.Tasks;
using Beeffective.Data.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class LoadAsync : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            var cellEntity = new CellEntity
            {
                Id = 1,
                Urgency = 2,
                Importance = 3, 
                Title = "test cell"
            };
            Repository.Add(cellEntity);
        }

        [Test]
        public async Task LoadAsync_Cells_Contain_OneCell()
        {
            await Sut.LoadAsync();
            Sut.Cells.Count.Should().Be(1);
            var cellViewModel = Sut.Cells.First();
            cellViewModel.Id.Should().Be(1);
            cellViewModel.X.Should().Be(2);
            cellViewModel.Y.Should().Be(3);
            cellViewModel.Title.Should().Be("test cell");
        }
    }
}
