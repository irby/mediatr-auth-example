using Dappa.Core.Common.Pipeline;
using MediatR;

namespace Dappa.Core.Features.Posts.Create;

public class CreatePostCommand : SecureRequest, IRequest<CreatePostCommandResponse>
{
    public string? Message { get; set; }
}
