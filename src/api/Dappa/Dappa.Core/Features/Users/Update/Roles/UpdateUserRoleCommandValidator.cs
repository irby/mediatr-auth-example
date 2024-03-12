using Dappa.Core.Common.Pipeline;
using FluentValidation;

namespace Dappa.Core.Features.Users.Update.Roles;

public class UpdateUserRoleCommandValidator : SecureRequestValidator<UpdateUserRoleCommand>
{
    public UpdateUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.UserId).NotEqual(x => x.ActingUserId);
        RuleFor(x => x.Role).IsInEnum();
        AllowOnlyAdmins();
    }
}
