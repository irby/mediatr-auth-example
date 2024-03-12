using Dappa.Core.Exceptions;
using Dappa.Core.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dappa.Core.Features.Users.Update.Roles;

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandHandler(AppUnitOfWork db)
    {
        _db = db;
    }
    
    private readonly AppUnitOfWork _db;
    
    public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException($"User Id '{request.UserId}' not found.");
        }
        
        user.Role = request.Role;
        await _db.SaveChangesAsync(cancellationToken);
    }
}
