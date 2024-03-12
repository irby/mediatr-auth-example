namespace Dappa.Core.Exceptions;

public class NotFoundException : RequestException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException()
    {
    }
    
    public override string StatusCode => "Not Found";
    public override int HttpCode => 404;
}
