using Cobs.Application.UseCases.Order.Commands.CreateOrder;
using Cobs.Application.UseCases.Order.Queries.GetAllOrders;
using Cobs.Application.UseCases.Order.Queries.GetCurrentUserOrders;
using Cobs.Application.UseCases.Order.Queries.GetOrdersByCustomerId;

namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateOrderCommand> _createValidator;
        private readonly IValidator<GetOrdersByCustomerIdQuery> _getByCustomerValidator;

        public OrderController
            (
            IMediator mediator,
            IValidator<CreateOrderCommand> createValidator,
            IValidator<GetOrdersByCustomerIdQuery> getByCustomerValidator
            )
        {
            _mediator = mediator;
            _createValidator = createValidator;
            _getByCustomerValidator = getByCustomerValidator;
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Manager")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            command = command with { CustomerId = this.GetCurrentUserId() };

            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    Message = "ورودی نامعتبر است",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByIdForManager), new { id = result }, new { Id = result });
        }

        [HttpGet("my-orders")]
        [Authorize(Roles = "Customer,Manager")]
        public async Task<IActionResult> GetMyOrders(CancellationToken cancellationToken)
        {
            int currentUserId = this.GetCurrentUserId();
            var result = await _mediator.Send(new GetCurrentUserOrdersQuery(currentUserId), cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("customers/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetByCustomerId([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetOrdersByCustomerIdQuery(id);
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

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetByIdForManager([FromRoute] int id)
        {
            return Ok(new { Message = $"جزئیات سفارش {id}" });
        }
    }
}
