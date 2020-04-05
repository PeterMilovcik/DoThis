using Beeffective.Models;
using NUnit.Framework;

namespace Beeffective.Tests.Models.CellModelTests
{
    class TestFixture
    {
        protected string ChangedPropertyName;
        protected const string Title = "test cell title";

        [SetUp]
        public virtual void SetUp()
        {
            Sut = new CellModel();
            Sut.PropertyChanged += (sender, args) =>
                ChangedPropertyName = args.PropertyName;
        }

        protected CellModel Sut { get; set; }
    }
}
