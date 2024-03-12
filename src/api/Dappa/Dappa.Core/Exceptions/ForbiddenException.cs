namespace Dappa.Core.Exceptions;

public class ForbiddenException : RequestException
{
    public ForbiddenException(string message) : base(message)
    {
    }
    
    public override string StatusCode => "Forbidden";
    public override int HttpCode => 403;
}
