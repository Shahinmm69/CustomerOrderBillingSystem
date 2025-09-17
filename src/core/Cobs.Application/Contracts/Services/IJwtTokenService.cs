namespace Cobs.Application.Contracts.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Customer user);
    }
}
