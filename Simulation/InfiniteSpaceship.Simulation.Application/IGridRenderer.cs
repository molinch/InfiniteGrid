using System.Collections.Generic;
using System.IO;
using InfiniteSpaceship.Simulation.DomainModel;

namespace InfiniteSpaceship.Simulation.Application
{
    public interface IGridRenderer
    {
        string ContentType { get; }

        bool Handles(string outputType);
        Stream Transform(Grid grid);
    }
}