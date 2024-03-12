using FluentValidation.Results;

namespace Dappa.Core.Exceptions;

public class BadRequestException : RequestException
{
    public BadRequestException(IEnumerable<ValidationFailure> errors)
    {
        Errors = errors;
    }

    public readonly IEnumerable<ValidationFailure> Errors;
    public override string StatusCode => "Bad Request";
    public override int HttpCode => 400;
}
