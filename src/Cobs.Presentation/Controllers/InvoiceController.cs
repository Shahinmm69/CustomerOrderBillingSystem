using Cobs.Application.UseCases.Invoice.Commands.PayInvoice;
using Cobs.Application.UseCases.Invoice.Queries.GetAllInvoices;
using Cobs.Application.UseCases.Invoice.Queries.GetCurrentUserInvoices;
using Cobs.Application.UseCases.Invoice.Queries.GetInvoicesByCustomerId;

namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICobsDbContext _context;
        private readonly IValidator<GetInvoicesByCustomerIdQuery> _getByCustomerValidator;

        public InvoiceController
            (
            IMediator mediator,
            ICobsDbContext context,
            IValidator<GetInvoicesByCustomerIdQuery> getByCustomerValidator
            )
        {
            _mediator = mediator;
            _context = context;
            _getByCustomerValidator = getByCustomerValidator;
        }

        [HttpGet("my-invoices")]
        [Authorize(Roles = "Customer,Manager")]
        public async Task<IActionResult> GetMyInvoices(CancellationToken cancellationToken)
        {
            int currentUserId = this.GetCurrentUserId();
            var result = await _mediator.Send(new GetCurrentUserInvoicesQuery(currentUserId), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllInvoicesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("customers/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetByCustomerId([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetInvoicesByCustomerIdQuery(id);
            var validationResult = await _getByCustomerValidator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    Message = "ورودی نامعتبر است",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{Id}/pay")]
        [Authorize(Roles = "Customer,Manager")]
        public async Task<IActionResult> PayInvoice([FromRoute] int Id, CancellationToken cancellationToken)
        {
            int currentUserId = this.GetCurrentUserId();
            var command = new PayInvoiceCommand(Id, currentUserId);
            await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "فاکتور با موفقیت پرداخت شد." });
        }
    }
}
