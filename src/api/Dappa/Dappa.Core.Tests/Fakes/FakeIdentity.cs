using System.Security.Principal;

namespace Dappa.Core.Tests.Fakes;

public class FakeIdentity : IIdentity
{
    public string AuthenticationType { get; set; }

    public bool IsAuthenticated { get; set; } = true;

    public string Name { get; set; } = "TEST USER";
}
