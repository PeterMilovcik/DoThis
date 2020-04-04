using Beeffective.Data;
using Beeffective.Data.Entities;
using NUnit.Framework;

namespace Beeffective.Tests.Data.CellRepositoryTests
{
    class TestFixture
    {
        protected CellEntity CellEntity1 { get; private set; }
        protected CellEntity CellEntity2 { get; private set; }
        protected CellEntity CellEntity3 { get; private set; }
        protected CellRepository Sut { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            Sut = new CellRepository();
            Sut.RemoveAll();
            CellEntity1 = new CellEntity {Title = "test cell 1"};
            CellEntity2 = new CellEntity {Title = "test cell 2"};
            CellEntity3 = new CellEntity {Title = "test cell 3"};
        }
    }
}
