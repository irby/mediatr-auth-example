using Dappa.Core.Common.Pipeline;
using Dappa.Core.Models.Enums;
using MediatR;

namespace Dappa.Core.Features.Users.Update.Roles;

public class UpdateUserRoleCommand : SecureRequest, IRequest
{
    public Guid UserId { get; set; }
    public Role Role { get; set; }
}
