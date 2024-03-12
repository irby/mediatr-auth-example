using System.Security.Principal;
using Dappa.Core.Common.Pipeline;
using Dappa.Core.Models;

namespace Dappa.Core.Tests.Extensions;

internal static class UnitTestExtensions
{
    internal static void SetActingUserAndIdentity<T>(this T request, User user, IIdentity identity) where T : SecureRequest
    {
        request.SetActingUser(user);
        request.SetActingIdentity(identity);
    }
}
