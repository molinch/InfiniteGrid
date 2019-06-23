using System;

namespace InfiniteSpaceship.Simulation.DomainModel
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
            return (Direction)Math.Abs(((int)direction - 1) % 4);
        }
    }
}