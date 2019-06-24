using System;

namespace InfiniteGrid.Simulation.DomainModel
{
    public enum Direction: byte
    {
        North,
        East,
        South,
        West
    }

    public static class DirectionExtensions
    {
        public static Direction MoveClockwise(this Direction direction)
        {
            return (Direction)(((int)direction + 1) % 4);
        }

        public static Direction MoveCounterClockwise(this Direction direction)
        {
            if (direction == Direction.North)
                return Direction.West;

            return (Direction)((int)direction - 1);
        }
    }
}