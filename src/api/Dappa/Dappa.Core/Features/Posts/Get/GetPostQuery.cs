using Dappa.Core.Common.Pipeline;
using Dappa.Core.Models.Dtos;
using MediatR;

namespace Dappa.Core.Features.Posts.Get;

public sealed class GetPostQuery : SecureRequest, IRequest<PostDto>
{
    public Guid Id { get; set; }
}
