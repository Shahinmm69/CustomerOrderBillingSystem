using Cobs.Application.UseCases.Auth.Commands;

namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Login ساده مبتنی بر ایمیل (فقط برای تست)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(new AuthQuery(request.Email), cancellationToken);
            return Ok(new { Token = token });
        }

        public record LoginRequest(string Email);
    }
}
