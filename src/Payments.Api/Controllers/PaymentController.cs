using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payments.Domain.Command.Payment;
using Payments.Domain.Queries.Payment;

namespace Payments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(

            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] PaymentCommand command)
        {
            var result = await _mediator.Send(command);

            return Created($"{nameof(PostAsync)}", result);
        }

        [HttpGet("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid transactionId)
        {
            var result = await _mediator.Send(new GetPaymentByTransactionIdQuery(transactionId));

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
