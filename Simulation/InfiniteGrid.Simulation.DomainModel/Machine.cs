using System.Collections.Generic;
using System.Drawing;

namespace InfiniteGrid.Simulation.DomainModel
{
    public class Machine
    {
        private readonly Dictionary<Point, ColoredPoint> _points;

        public static IReadOnlyDictionary<Direction, Size> Moves => new Dictionary<Direction, Size>()
        {
            { Direction.North, new Size( 0,  1) },
            { Direction.East,  new Size( 1,  0) },
            { Direction.South, new Size( 0, -1) },
            { Direction.West,  new Size(-1,  0) }
        };
        
        public Machine(ColoredPoint from, Direction faces)
        {
            Current = from;
            Faces = faces;
            _points = new Dictionary<Point, ColoredPoint>();
        }

        public ColoredPoint Current { get; private set; }
        public Direction Faces { get; private set; }
        public IEnumerable<ColoredPoint> Points => _points.Values;

        public IEnumerable<Size> KeepMoving()
        {
            while (true)
            {
                yield return Move();
            }
        }

        /// <summary>
        /// Moves the machine to the next cell
        /// </summary>
        /// <returns>The translation from original cell to destination cell</returns>
        public Size Move()
        {
            var current = Current;
            var faces = Faces;
            faces = Move(faces, current.IsBlack);
            _points[current.Point] = current.Flip();
            var move = Moves[faces];
            Current = GetOrCreateFor(current.Point + move);
            Faces = faces;
            return move;
        }

        private ColoredPoint GetOrCreateFor(Point point)
        {
            if (!_points.TryGetValue(point, out var coloredPoint))
            {
                coloredPoint = new ColoredPoint(point);
                _points.Add(point, coloredPoint);
            }

            return coloredPoint;
        }

        private Direction Move(Direction direction, bool isBlack)
        {
            return isBlack ? direction.MoveCounterClockwise() : direction.MoveClockwise();
        }
    }
}
