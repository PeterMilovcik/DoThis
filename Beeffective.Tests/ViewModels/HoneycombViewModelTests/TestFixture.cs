using Beeffective.ViewModels;
using NUnit.Framework;

namespace Beeffective.Tests.ViewModels.HoneycombViewModelTests
{
    class TestFixture
    {
        public HoneycombViewModel Sut { get; private set; }

        [SetUp]
        public virtual void SetUp()
        {
            Sut = new HoneycombViewModel();
        }
    }
}
