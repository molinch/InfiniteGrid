using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace InfiniteSpaceship.Simulation.DomainModel
{
    public class Grid
    {
        private readonly IReadOnlyDictionary<Point, ColoredPoint> _coloredPoints;

        public Grid(IEnumerable<ColoredPoint> points)
        {
            _coloredPoints = points.ToDictionary(p => p.Point, p => p);
            if (_coloredPoints.Count == 0)
            {
                return;
            }

            var firstPoint = _coloredPoints.Values.First().Point;
            var left = firstPoint.X;
            var right = firstPoint.X;
            var top = firstPoint.Y;
            int bottom = firstPoint.Y;
            foreach (var point in _coloredPoints.Values)
            {
                left = Math.Min(left, point.Point.X);
                right = Math.Max(right, point.Point.X);
                top = Math.Max(top, point.Point.Y);
                bottom = Math.Min(bottom, point.Point.Y);
            }

            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }

        public IEnumerable<ColoredPoint> AllPointsFromTopLeftToBottomRight()
        {
            for (var i = Left; i < Right; i++)
            {
                for (var j = Bottom; j < Top; j++)
                {
                    var point = new Point(i, j);
                    if (!_coloredPoints.TryGetValue(point, out var coloredPoint))
                    {
                        coloredPoint = new ColoredPoint(point);
                    }
                    yield return coloredPoint;
                }
            }
        }

        public IEnumerable<IEnumerable<ColoredPoint>> AllPointsRowByRow()
        {
            return AllPointsFromTopLeftToBottomRight()
                .GroupBy(p => p.Point.X);
        }
    }
}
