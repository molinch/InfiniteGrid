using NUnit.Framework;
using System.Drawing;
using System.Linq;

namespace InfiniteGrid.Simulation.DomainModel.Tests
{
    public class GridTests
    {
        [Test]
        public void Given_points_Has_correct_dimension()
        {
            var sut = new Grid(new[]
            {
                new ColoredPoint(new Point(0, 0), true),
                new ColoredPoint(new Point(5, 2)),
                new ColoredPoint(new Point(3, -7), true),
                new ColoredPoint(new Point(-8, -1)),
            });

            Assert.AreEqual(2, sut.Top);
            Assert.AreEqual(-7, sut.Bottom);
            Assert.AreEqual(-8, sut.Left);
            Assert.AreEqual(5, sut.Right);
        }
        
        [Test]
        public void Given_points_Iterate_through_grid_points()
        {
            var sut = new Grid(new[]
            {
                new ColoredPoint(new Point(0, 0), true),
                new ColoredPoint(new Point(5, 2)),
                new ColoredPoint(new Point(3, -7), true),
                new ColoredPoint(new Point(-8, -1)),
            });

            var all = sut.AllPointsFromTopLeftToBottomRight().ToList();
            
            foreach (var point in all)
            {
                if (point.Point == new Point(0, 0)
                    || point.Point == new Point(3, -7))
                {
                    Assert.IsTrue(point.IsBlack);
                }
                else
                {
                    Assert.IsFalse(point.IsBlack);
                }
            }
        }
    }
}
