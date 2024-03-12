using Dappa.Core.Features.Users.Update.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dappa.Api.Controllers;

public class UsersController : BaseController<UsersController>
{
    public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(logger, mediator)
    {
    }
    
    [HttpPut("{id}/role")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] Guid id, [FromBody] UpdateUserRoleCommand command)
    {
        command.UserId = id;
        await Mediator.Send(command);
        return Ok();
    }
}
