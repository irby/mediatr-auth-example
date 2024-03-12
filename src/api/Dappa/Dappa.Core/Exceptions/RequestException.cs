namespace Dappa.Core.Exceptions;

public abstract class RequestException : Exception
{
    public static string ClassName => "RequestException";
    protected RequestException(string message) : base(message) {}
    protected RequestException() {}
    public abstract string StatusCode { get; }
    public abstract int HttpCode { get; }
    public string Type => ClassName;
}
