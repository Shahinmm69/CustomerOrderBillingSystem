namespace Cobs.Application.UseCases.Auth.Commands
{
    public record AuthQuery(string Email) : IRequest<string>;
}
