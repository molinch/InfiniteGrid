using NUnit.Framework;

namespace InfiniteGrid.Simulation.DomainModel.Tests
{
    public class DirectionTests
    {
        [Test]
        public void Given_north_Move_clockwise_to_east()
        {
            var source = Direction.North;
            var result = source.MoveClockwise();
            Assert.AreEqual(Direction.East, result);
        }

        [Test]
        public void Given_west_Move_clockwise_to_north()
        {
            var source = Direction.West;
            var result = source.MoveClockwise();
            Assert.AreEqual(Direction.North, result);
        }

        [Test]
        public void Given_north_Move_counter_clockwise_to_west()
        {
            var source = Direction.North;
            var result = source.MoveCounterClockwise();
            Assert.AreEqual(Direction.East, result);
        }

        [Test]
        public void Given_west_Move_counterclockwise_to_south()
        {
            var source = Direction.West;
            var result = source.MoveCounterClockwise();
            Assert.AreEqual(Direction.South, result);
        }
    }
}
