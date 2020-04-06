using Beeffective.ViewModels;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class TestFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            Sut = new HoneycombViewModel();
            Sut.PropertyChanged += (sender, args) =>
                ChangedPropertyName = args.PropertyName;
        }

        protected HoneycombViewModel Sut { get; private set; }

        protected string ChangedPropertyName { get; set; }
    }
}
