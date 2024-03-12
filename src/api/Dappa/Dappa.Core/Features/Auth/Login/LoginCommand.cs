using MediatR;

namespace Dappa.Core.Features.Auth.Login;

public class LoginCommand : IRequest<LoginCommandResponse>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
