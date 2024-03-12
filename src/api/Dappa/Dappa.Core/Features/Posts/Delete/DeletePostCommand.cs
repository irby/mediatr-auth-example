using Dappa.Core.Common.Pipeline;
using MediatR;

namespace Dappa.Core.Features.Posts.Delete;

public class DeletePostCommand : SecureRequest, IRequest
{
    public Guid Id { get; set; }
}
