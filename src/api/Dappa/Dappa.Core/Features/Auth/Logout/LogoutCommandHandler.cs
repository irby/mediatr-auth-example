using MediatR;
using Microsoft.Extensions.Logging;

namespace Dappa.Core.Features.Auth.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly ILogger<LogoutCommandHandler> _logger;
    
    public LogoutCommandHandler(ILogger<LogoutCommandHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"User {request.ActingUserId} has logged out");
    }
}
