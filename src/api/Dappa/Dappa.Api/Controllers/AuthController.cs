using Dappa.Api.Managers;
using Dappa.Core.Features.Auth.Login;
using Dappa.Core.Features.Auth.Logout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dappa.Api.Controllers;

public class AuthController : BaseController<AuthController>
{
    private readonly CookieManager _cookieManager;

    public AuthController(ILogger<AuthController> logger, IMediator mediator, CookieManager cookieManager) : base(
        logger, mediator)
    {
        _cookieManager = cookieManager;
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await Mediator.Send(command);
        if (response.User is null)
        {
            return NotFound();
        }
        
        _cookieManager.SetCookies(Response, response.User);
        
        return Ok(response);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await Mediator.Send(new LogoutCommand());
        return Ok();
    }
}
