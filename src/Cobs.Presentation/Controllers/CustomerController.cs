using Cobs.Application.UseCases.Customer.Commands.CreateCustomer;
using Cobs.Application.UseCases.Customer.Queries.GetAllCustomers;
using Cobs.Application.UseCases.Customer.Queries.GetCurrentUserCustomer;
using Cobs.Application.UseCases.Customer.Queries.GetCustomerById;

namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateCustomerCommand> _createValidator;
        private readonly IValidator<GetCustomerByIdQuery> _getByIdValidator;

        public CustomerController
            (
            IMediator mediator,
            IValidator<CreateCustomerCommand> createValidator,
            IValidator<GetCustomerByIdQuery> getByIdValidator
            )
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _getByIdValidator = getByIdValidator;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    Message = "ورودی نامعتبر است",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { Id = result });
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "Customer,Manager")]
        public async Task<IActionResult> GetMyProfile(CancellationToken cancellationToken)
        {
            int currentUserId = this.GetCurrentUserId();
            var result = await _mediator.Send(new GetCurrentUserCustomerQuery(currentUserId), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdQuery(id);
            var validationResult = await _getByIdValidator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    Message = "ورودی نامعتبر است",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
