using MediatR;

namespace Dappa.Core.Features.Auth.Login;

public sealed class LoginCommand : IRequest<LoginCommandResponse>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
