using Dappa.Core.Models;
using Dappa.Core.Models.Dtos;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dappa.Core.Features.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly AppUnitOfWork _db;
    public LoginCommandHandler(ILogger<LoginCommandHandler> logger, AppUnitOfWork unitOfWork)
    {
        _logger = logger;
        _db = unitOfWork;
    }
    
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Request received: ${request}");
        
        var user = await _db.Users.SingleOrDefaultAsync(p => p.Username == request.Username, cancellationToken);

        if (user is null)
        {
            throw new Exception("user does not exist");
        }

        if (user.Password != request.Password)
        {
            throw new Exception("incorrect password");
        }
        
        var dto = UserDto.FromUser(user);
        
        return new LoginCommandResponse()
        {
            User = dto
        };
    }
}
