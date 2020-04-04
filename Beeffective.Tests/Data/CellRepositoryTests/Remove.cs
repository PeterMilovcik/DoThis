using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class Remove : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            Sut.Add(CellEntity1);
        }

        [Test]
        public async Task DoesNotContain_Removed_CellEntity()
        {
            Sut.Remove(CellEntity1);
            var cellEntities = await Sut.LoadAsync();
            cellEntities.Should().NotContain(CellEntity1);
        }
    }
}
