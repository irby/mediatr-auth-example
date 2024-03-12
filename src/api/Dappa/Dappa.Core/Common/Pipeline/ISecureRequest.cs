using System.Security.Principal;
using Dappa.Core.Models;
using Dappa.Core.Models.Enums;

namespace Dappa.Core.Common.Pipeline;

public interface ISecureRequest
{
    public Guid ActingUserId { get; }
    public Role ActingUserRole { get; }
    public IIdentity? ActingIdentity { get; }
    public void SetActingUser(User user);
    public void SetActingIdentity(IIdentity identity);
}
