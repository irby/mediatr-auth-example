using MediatR;

namespace Dappa.Core.Common.Pipeline;

public abstract class SecureRequest : ISecureRequest, IRequest
{
    private Guid _userId;
    public Guid GetUserId() => _userId;

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }
}
