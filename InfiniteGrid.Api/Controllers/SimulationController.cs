using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using InfiniteGrid.Simulation.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfiniteGrid.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly IMediator mediator;

        public SimulationController(IMediator mediator)
        {
            Guard.Against.Null(mediator, nameof(mediator));

            this.mediator = mediator;
        }

        [HttpPut("{numberSteps}")]
        public async Task<ActionResult> RunSimulation(int? numberSteps)
        {
            var validationResult = ValidateNumberSteps(numberSteps);

            if (validationResult != null)
            {
                return validationResult;
            }

            //TODO if calculation is done in background return "processing" if not yet calculated 

            var simulationResult = await mediator.Send(new SimulationRequest { NumberSteps = numberSteps.Value, OutputType = "text" });

            if (simulationResult.Stream == null)
            {
                return StatusCode(500, "Simulation could not transform result correctly");
            }

            //TODO slow to serialize big files directly, could save it async locally and return link for download
            return new FileStreamResult(simulationResult.Stream, simulationResult.ContentType); 
        }

        private ActionResult ValidateNumberSteps(int? numberSteps)
        {
            if (!numberSteps.HasValue)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Number of steps needs to be specified");
            }
            if (numberSteps.Value <= 0)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Number of steps needs to be positive value");
            }
            return null;
        }
    }
}