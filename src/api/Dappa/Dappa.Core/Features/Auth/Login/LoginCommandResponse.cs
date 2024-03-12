using Dappa.Core.Models.Dtos;

namespace Dappa.Core.Features.Auth.Login;

public class LoginCommandResponse
{
    public UserDto? User { get; set; }
}
