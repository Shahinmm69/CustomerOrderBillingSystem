namespace Cobs.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICobsDbContext _context;
        private readonly IJwtTokenService _jwt;

        public AuthController(ICobsDbContext context, IJwtTokenService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        // Login ساده مبتنی بر ایمیل (فقط برای تست)
        public record LoginRequest(string Email);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);
            if (user == null) return Unauthorized(new { Message = "ایمیل نامعتبر است" });

            var token = _jwt.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
