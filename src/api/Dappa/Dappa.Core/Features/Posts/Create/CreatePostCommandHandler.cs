using Dappa.Core.Models;
using Dappa.Core.UnitsOfWork;
using MediatR;

namespace Dappa.Core.Features.Posts.Create;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostCommandResponse>
{
    private readonly AppUnitOfWork _db;
    
    public CreatePostCommandHandler(AppUnitOfWork db)
    {
        _db = db;
    }
    
    public async Task<CreatePostCommandResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Message = request.Message,
            UserId = request.ActingUserId
        };
        
        await _db.Posts.AddAsync(post, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        
        return new CreatePostCommandResponse()
        {
            Id = post.Id
        };
    }
}
