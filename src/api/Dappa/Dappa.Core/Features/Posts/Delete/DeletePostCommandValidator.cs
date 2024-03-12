using Dappa.Core.Common.Pipeline;
using FluentValidation;

namespace Dappa.Core.Features.Posts.Delete;

public class DeletePostCommandValidator : SecureRequestValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
