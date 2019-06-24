using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace InfiniteGrid.Simulation.DomainModel
{
    public class MachineCycleAware
    {
        private readonly Machine _machine;
        private readonly CycleFinder<Size> _cycleFinder;
        private const int CONSIDER_CYCLE_IF_MATCHES_MOVES = 100;

        public MachineCycleAware(Machine machine)
        {
            _machine = machine;
            _cycleFinder = new CycleFinder<Size>((a, b) => a.Equals(b));
        }

        public void Move(int times)
        {
            var items = _machine.KeepMoving().Take(times).ToList();
            _cycleFinder.Read(items);
        }

        public ColoredPoint Current => _machine.Current;

        public Direction Faces => _machine.Faces;

        public IEnumerable<ColoredPoint> Points => _machine.Points;
    }
}
