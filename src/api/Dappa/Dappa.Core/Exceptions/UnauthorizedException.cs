namespace Dappa.Core.Exceptions;

public class UnauthorizedException : RequestException
{

    public UnauthorizedException(string message) : base(message)
    {
        
    }

    public override string StatusCode => "Unauthorized";
    public override int HttpCode => 401;
}
