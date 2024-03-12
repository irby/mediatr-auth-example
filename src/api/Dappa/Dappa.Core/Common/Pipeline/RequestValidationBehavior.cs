using Dappa.Core.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Dappa.Core.Common.Pipeline;

public class RequestValidationBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
{
    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> requestValidators)
    {
        RequestValidators = requestValidators;
    }
    
    private IEnumerable<IValidator<TRequest>> RequestValidators { get; }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = new List<ValidationFailure>();
        foreach (var requestValidator in RequestValidators)
        {
            var validationResult = await requestValidator.ValidateAsync(context, cancellationToken);
            var errors = validationResult.Errors.Where(p => p != null);
            failures.AddRange(errors);
        }
        
        if (failures.Any(p => p.ErrorCode == ErrorCodes.UnauthorizedCode))
            throw new UnauthorizedException(failures.First(p => p.ErrorCode == ErrorCodes.UnauthorizedCode).ErrorMessage);
        
        if (failures.Any(p => p.ErrorCode == ErrorCodes.ForbiddenCode))
            throw new ForbiddenException(failures.First(p => p.ErrorCode == ErrorCodes.ForbiddenCode).ErrorMessage);

        if (failures.Count != 0)
            throw new BadRequestException(failures);

        return await next();
    }
}
