using Cobs.Application.UseCases.Wallet.Commands.Deposit;

namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<DepositCommand> _depositValidator;

        public WalletController
            (
            IMediator mediator,
            IValidator<DepositCommand> depositValidator
            )
        {
            _mediator = mediator;
            _depositValidator = depositValidator;
        }

        [HttpPost("deposit")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _depositValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(new
                {
                    Message = "ورودی نامعتبر است",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
                });

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new
            {
                Message = "مبلغ با موفقیت واریز شد.",
                WalletBalance = result
            });
        }
    }
}