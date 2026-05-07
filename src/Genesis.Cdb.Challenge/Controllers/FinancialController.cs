using Genesis.Cdb.Challenge.Application.Commands;
using Genesis.Cdb.Challenge.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Cdb.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("calculate")]
        [ProducesResponseType(
            typeof(CdbCalculationResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CdbCalculationResponse>>
            Calculate(
                [FromBody] CalculateCdbCommand command,
                CancellationToken cancellationToken)
        {
            var result =
                await _mediator.Send(
                    command,
                    cancellationToken);

            return Ok(result);
        }
    }
}
