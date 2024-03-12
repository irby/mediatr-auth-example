using Dappa.Core.Common.Pipeline;
using FluentValidation;

namespace Dappa.Core.Features.Posts.Create;

public class CreatePostCommandValidator : SecureRequestValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(p => p.Message)
            .NotEmpty()
            .MaximumLength(150);
    }
}
