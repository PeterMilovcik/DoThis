using Beeffective.Data;
using Beeffective.Models;

namespace Beeffective.Services
{
    public class HoneycombService
    {
        public HoneycombService()
        {
            Honeycomb = new HoneycombModel(new CellRepository());
        }

        public HoneycombModel Honeycomb { get; }
    }
}
