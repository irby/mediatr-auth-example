using System.Security.Principal;
using Dappa.Core.Models;
using Dappa.Core.Models.Enums;

namespace Dappa.Core.Common.Pipeline;

public abstract class SecureRequest : ISecureRequest
{
    public Guid ActingUserId { get; private set; }

    public Role ActingUserRole { get; private set; }

    public IIdentity? ActingIdentity { get; private set; }

    public void SetActingUser(User user)
    {
        ActingUserId = user.Id;
        ActingUserRole = user.Role;
    }

    public void SetActingIdentity(IIdentity identity)
    {
        ActingIdentity = identity;
    }
}
