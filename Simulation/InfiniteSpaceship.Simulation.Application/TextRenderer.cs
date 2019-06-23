using InfiniteSpaceship.Simulation.DomainModel;
using System;
using System.IO;
using System.Text;

namespace InfiniteSpaceship.Simulation.Application
{
    public class TextRenderer : IGridRenderer
    {
        public bool Handles(string outputType) => string.Equals(outputType, "text", StringComparison.OrdinalIgnoreCase);

        public string ContentType => "text/plain";

        public Stream Transform(Grid grid)
        {
            var sb = new StringBuilder();
            foreach (var row in grid.AllPointsRowByRow())
            {
                foreach (var point in row)
                {
                    sb.Append(point.IsBlack ? 'X' : ' ');
                }
                sb.AppendLine();
            }
            
            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }
    }
}
