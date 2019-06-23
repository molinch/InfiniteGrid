using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InfiniteSpaceship.Simulation.Application.Responses
{
    public class SimulationResponse

    {
        public Stream Stream { get; set; }

        public string ContentType { get; set; }
    }
}
