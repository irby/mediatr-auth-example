using Dappa.Core.Features.Posts.Create;
using Dappa.Core.Features.Posts.Delete;
using Dappa.Core.Features.Posts.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dappa.Api.Controllers;

public class PostsController : BaseController<PostsController>
{
    public PostsController(ILogger<PostsController> logger, IMediator mediator) : base(logger, mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost([FromRoute] Guid id)
    {
        var query = new GetPostQuery { Id = id };
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] Guid id)
    {
        var command = new DeletePostCommand { Id = id };
        await Mediator.Send(command);
        return Ok();
    }
}
