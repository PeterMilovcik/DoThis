using Beeffective.Data.Entities;
using NUnit.Framework;

namespace Beeffective.Tests.Data.Entities.CellEntityTests
{
    class TestFixture
    {
        protected const string Title = "test cell entity";
        protected const double Importance = 100;
        protected const double Urgency = 200;

        [SetUp]
        public virtual void SetUp()
        {
            Sut = new CellEntity();
        }

        protected CellEntity Sut { get; set; }
    }
}
