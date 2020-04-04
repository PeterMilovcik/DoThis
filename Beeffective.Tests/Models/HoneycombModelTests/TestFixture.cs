using Beeffective.Models;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class TestFixture
    {
        protected HoneycombModel Sut { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Sut = new HoneycombModel();
        }
    }
}