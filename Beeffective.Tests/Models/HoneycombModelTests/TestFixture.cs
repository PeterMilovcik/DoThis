using Beeffective.Data;
using Beeffective.Data.Entities;
using Beeffective.Models;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class TestFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            Repository = new CellRepository();
            Repository.RemoveAll();
            Sut = new HoneycombModel(Repository);
            CellModel1 = new CellModel {Title = "test cell model 1"};
            CellModel2 = new CellModel {Title = "test cell model 2"};
            CellModel3 = new CellModel {Title = "test cell model 3"};
        }

        protected CellModel CellModel1 { get; set; }

        protected CellModel CellModel2 { get; set; }

        protected CellModel CellModel3 { get; set; }

        protected HoneycombModel Sut { get; set; }

        protected IRepository<CellEntity> Repository { get; set; }
    }
}