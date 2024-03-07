namespace Dappa.Core.Common.Pipeline;

public interface ISecureRequest
{
    public Guid GetUserId();
    public void SetUserId(Guid userId);
}