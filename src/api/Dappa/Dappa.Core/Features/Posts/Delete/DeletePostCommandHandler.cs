using Dappa.Core.Exceptions;
using Dappa.Core.Models.Enums;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dappa.Core.Features.Posts.Delete;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    public DeletePostCommandHandler(AppUnitOfWork db)
    {
        _db = db;
    }

    private readonly AppUnitOfWork _db;
    
    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (post is null)
        {
            throw new NotFoundException($"Post Id '{request.Id}' not found.");
        }
        
        if (post.UserId != request.ActingUserId && request.ActingUserRole != Role.Admin)
        {
            throw new ForbiddenException("You are not allowed to delete this post.");
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
