using NUnit.Framework;
using System.Drawing;
using System.Linq;

namespace InfiniteGrid.Simulation.DomainModel.Tests
{
    public class CycleFinderTests
    {
        [Test]
        public void Test()
        {
            var sut = new CycleFinder<Size>((a, b) => a.Equals(b));
            var machine = new Machine(ColoredPoint.Origin, Direction.East);
            var items = machine.KeepMoving().Take(20000).ToList();
            sut.Read(items);
        }
    }
}
