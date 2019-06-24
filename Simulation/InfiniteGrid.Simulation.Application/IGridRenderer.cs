using System.Collections.Generic;
using System.IO;
using InfiniteGrid.Simulation.DomainModel;

namespace InfiniteGrid.Simulation.Application
{
    public interface IGridRenderer
    {
        string ContentType { get; }

        bool Handles(string outputType);
        Stream Transform(Grid grid);
    }
}