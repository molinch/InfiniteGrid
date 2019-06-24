using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfiniteGrid.Simulation.Application.Responses
{
    public class SimulationResponse

    {
        public Stream Stream { get; set; }

        public string ContentType { get; set; }
    }
}
