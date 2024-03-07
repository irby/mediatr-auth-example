using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dappa.Api.Controllers;

[Produces("application/json")]
[Authorize]
public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
{
    public BaseController(ILogger<T> logger, IMediator mediator)
    {
        Logger = logger;
        Mediator = mediator;
    }
    
    protected ILogger<T> Logger { get; }
    protected IMediator Mediator { get; }
}
