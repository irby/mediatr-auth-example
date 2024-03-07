using Dappa.Core.Common.Pipeline;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dappa.Api.Behaviors;

public class SecureRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ISecureRequest
{
    public SecureRequestBehavior(IHttpContextAccessor httpContextAccessor, AppUnitOfWork appUnitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _appUnitOfWork = appUnitOfWork;
    }

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppUnitOfWork _appUnitOfWork;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userIdentity = _httpContextAccessor.HttpContext?.User?.Identity;

        var user = await _appUnitOfWork.Users.FirstOrDefaultAsync(p => p.Id == Guid.Parse(userIdentity.Name), cancellationToken);
        
        request.SetUserId(user.Id);
        return await next();
    }
}
