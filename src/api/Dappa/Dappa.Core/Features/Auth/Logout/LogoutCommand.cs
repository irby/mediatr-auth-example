using Dappa.Core.Common.Pipeline;
using MediatR;

namespace Dappa.Core.Features.Auth.Logout;

public class LogoutCommand : SecureRequest, IRequest
{
}
