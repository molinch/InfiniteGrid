using InfiniteSpaceship.Simulation.Application.Responses;
using InfiniteSpaceship.Simulation.DomainModel;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace InfiniteSpaceship.Simulation.Application.Requests
{
    public class SimulationRequest : IRequest<SimulationResponse>
    {
        [Required]
        public int NumberSteps { get; set; }

        public string OutputType { get; set; }

        public class SimulationRequestHandler : IRequestHandler<SimulationRequest, SimulationResponse>
        {
            private readonly IGridRendererFactory _rendererFactory;

            public SimulationRequestHandler(IGridRendererFactory rendererFactory)
            {
                _rendererFactory = rendererFactory;
            }

            public Task<SimulationResponse> Handle(SimulationRequest request, CancellationToken cancellationToken)
            {
                var machine = new Machine(ColoredPoint.Origin, Direction.East);
                for (var i=0; i < request.NumberSteps; i++)
                {
                    machine.Move();
                }

                var grid = new Grid(machine.Points);
                var renderer = _rendererFactory.Get(request.OutputType);
                var stream = renderer.Transform(grid);

                return Task.FromResult(new SimulationResponse { Stream = stream, ContentType = renderer.ContentType});
            }
        }
    }
}
