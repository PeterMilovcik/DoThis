using Beeffective.Data;
using Beeffective.Models;
using Beeffective.ViewModels;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class TestFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            Repository = new CellRepository();
            Repository.RemoveAll();
            Model = new HoneycombModel(Repository);
            Sut = new HoneycombViewModel(Model, null);
            Sut.PropertyChanged += (sender, args) =>
                ChangedPropertyName = args.PropertyName;
        }

        protected CellRepository Repository { get; set; }

        protected HoneycombModel Model { get; set; }

        protected HoneycombViewModel Sut { get; set; }

        protected string ChangedPropertyName { get; set; }
    }
}
