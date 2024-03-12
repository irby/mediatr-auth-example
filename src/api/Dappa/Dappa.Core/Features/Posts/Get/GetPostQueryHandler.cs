using Dappa.Core.Exceptions;
using Dappa.Core.Models.Dtos;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dappa.Core.Features.Posts.Get;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
{
    private AppUnitOfWork _db;
    
    public GetPostQueryHandler(AppUnitOfWork db)
    {
        _db = db;
    }
    
    public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _db.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        
        if (post is null)
        {
            throw new NotFoundException($"Post Id '{request.Id}' not found");
        }

        return PostDto.FromPost(post);
    }
}
