namespace Cobs.Application.UseCases.Auth.Commands
{
    public class AuthQueryHandler : IRequestHandler<AuthQuery, string>
    {
        private readonly ICobsDbContext _context;
        private readonly IJwtTokenService _jwt;

        public AuthQueryHandler(ICobsDbContext context, IJwtTokenService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<string> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == request.Email, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email");

            return _jwt.GenerateToken(user);
        }
    }
}
