using Dappa.Core.Models.Enums;
using FluentValidation;

namespace Dappa.Core.Common.Pipeline;

public abstract class SecureRequestValidator<TRequest> : AbstractValidator<TRequest> where TRequest : ISecureRequest
{
    private const string ActingIdentity = "ActingIdentity";
    private const string ActingIdentityName = "ActingIdentity.Name";
    private const string ActingIdentityIsAuthenticated = "ActingIdentity.IsAuthenticated";
    private const string ActingUserId = "ActingUserId";
    private const string ActingRole = "ActingRole";

    protected SecureRequestValidator()
    {
        // Require acting identity
        RuleFor(p => p.ActingIdentity)
            .NotEmpty()
            .OverridePropertyName(ActingIdentity)
            .WithErrorCode(ErrorCodes.UnauthorizedCode);

        When(
            p => p.ActingIdentity != null,
            () =>
            {
                // Name must not be empty
                RuleFor(p => p.ActingIdentity!.Name)
                    .NotEmpty()
                    .OverridePropertyName(ActingIdentityName)
                    .WithErrorCode(ErrorCodes.UnauthorizedCode);

                // Must be authenticated
                RuleFor(p => p.ActingIdentity!.IsAuthenticated)
                    .Equal(true)
                    .OverridePropertyName(ActingIdentityIsAuthenticated)
                    .WithErrorCode(ErrorCodes.UnauthorizedCode);
            });
        
        // Must be valid user id
        RuleFor(p => p.ActingUserId)
            .NotEmpty()
            .OverridePropertyName(ActingUserId)
            .WithErrorCode(ErrorCodes.UnauthorizedCode);

        // Must be valid role
        RuleFor(p => p.ActingUserRole)
            .IsInEnum()
            .OverridePropertyName(ActingRole)
            .WithErrorCode(ErrorCodes.UnauthorizedCode);
    }

    protected void AllowOnlyAdmins()
    {
        RuleFor(p => p.ActingUserRole)
            .Equal(Role.Admin)
            .OverridePropertyName(ActingRole)
            .WithErrorCode(ErrorCodes.ForbiddenCode);
    }
}
