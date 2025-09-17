using Cobs.Application.UseCases.Transaction.Queries.GetCurrentUserTransactions;

namespace Cobs.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer,Manager")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
        {
            int currentUserId = this.GetCurrentUserId();
            var transactions = await _mediator.Send(new GetTransactionsQuery(currentUserId), cancellationToken);
            return Ok(transactions);
        }
    }
}
