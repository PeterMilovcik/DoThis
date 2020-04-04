using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class Update : TestFixture
    {
        private const string ChangedTestTitle = "changed test title 1";

        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1);
            CellEntity1.Title = ChangedTestTitle;
            Sut.Update(CellEntity1);
        }

        [Test]
        public async Task CellEntity_IsUpdated()
        {
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().Contain(CellEntity1);
            CellEntity1.Title.Should().Be(ChangedTestTitle);
        }
    }
}
