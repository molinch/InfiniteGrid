using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteGrid.Simulation.Application
{
    public class GridRendererFactory : IGridRendererFactory
    {
        private readonly IReadOnlyList<IGridRenderer> _renderers;

        public GridRendererFactory(IEnumerable<IGridRenderer> renderers)
        {
            _renderers = renderers.ToList();
        }

        public IGridRenderer Get(string outputType)
        {
            return _renderers.FirstOrDefault(r => r.Handles(outputType))
                ?? throw new NotSupportedException($"unsupported output type : {outputType}");
        }
    }
}
