using System.Drawing;

namespace InfiniteGrid.Simulation.DomainModel
{
    public struct ColoredPoint
    {
        public static ColoredPoint Origin = new ColoredPoint(Point.Empty, false);

        public ColoredPoint(Point point, bool isBlack = false)
        {
            Point = point;
            IsBlack = isBlack;
        }

        public Point Point { get; }
        public bool IsBlack { get; }

        public ColoredPoint Flip() => new ColoredPoint(Point, !IsBlack);

        public override string ToString()
        {
            char color = IsBlack ? 'B' : 'W';
            return $"{{{Point.X}:{Point.Y}}}{color}"; 
        }
    }
}
