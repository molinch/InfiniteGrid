using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace InfiniteSpaceship.Simulation.DomainModel.Tests
{
    public class MachineTests
    {
        [Test]
        public void Given_created_Correctly_filled()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            Assert.AreEqual(ColoredPoint.Origin.Point, sut.Current.Point);
            Assert.AreEqual(Direction.East, sut.Faces);
        }

        [Test]
        public void Given_moves_once_from_origin_Has_two_points()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            sut.Move();
            sut.Points.Should().BeEquivalentTo(new[]
            {
                new ColoredPoint(new Point(0, 0), true),
                new ColoredPoint(new Point(0, -1), false),
            });
        }

        [Test]
        public void Given_moves_twice_from_origin_Has_three_points()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            sut.Move();
            sut.Move();
            sut.Points.Should().BeEquivalentTo(new[]
            {
                new ColoredPoint(new Point(0,   0), true),
                new ColoredPoint(new Point(0,  -1), true),
                new ColoredPoint(new Point(-1, -1), false),
            });
        }

        [Test]
        public void Given_moves_3_times_from_origin_Has_4_points()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            sut.Move();
            sut.Move();
            sut.Move();
            sut.Points.Should().BeEquivalentTo(new[]
            {
                new ColoredPoint(new Point(0,   0), true),
                new ColoredPoint(new Point(0,  -1), true),
                new ColoredPoint(new Point(-1, -1), true),
                new ColoredPoint(new Point(-1,  0), false),
            });
        }

        [Test]
        public void Given_moves_4_times_from_origin_Has_4_points()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            sut.Move(); //  0: 0  ->  0:-1    moves south
            sut.Move(); //  0:-1  -> -1:-1    moves west
            sut.Move(); // -1:-1  -> -1: 0    moves north
            sut.Move(); // -1: 0  ->  0: 0    moves east
            sut.Points.Should().BeEquivalentTo(new[]
            {
                new ColoredPoint(new Point(0,   0), true),
                new ColoredPoint(new Point(0,  -1), true),
                new ColoredPoint(new Point(-1, -1), true),
                new ColoredPoint(new Point(-1,  0), true),
            });
        }

        [Test]
        public void Given_moves_5_times_from_origin_Has_5_points_And_origin_reset_to_white()
        {
            var sut = new Machine(ColoredPoint.Origin, Direction.East);
            sut.Move(); //  0: 0  ->  0:-1    moves south
            sut.Move(); //  0:-1  -> -1:-1    moves west
            sut.Move(); // -1:-1  -> -1: 0    moves north
            sut.Move(); // -1: 0  ->  0: 0    moves east
            sut.Move(); //  0: 0  ->  0: 1    moves north / counter clockwise
            sut.Points.Should().BeEquivalentTo(new[]
            {
                new ColoredPoint(new Point(0,   0), false),
                new ColoredPoint(new Point(0,  -1), true),
                new ColoredPoint(new Point(-1, -1), true),
                new ColoredPoint(new Point(-1,  0), true),
                new ColoredPoint(new Point(0,   1), false),
            });
        }
    }
}
