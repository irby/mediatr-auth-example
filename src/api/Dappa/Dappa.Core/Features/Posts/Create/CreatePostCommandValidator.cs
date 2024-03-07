using FluentValidation;

namespace Dappa.Core.Features.Posts.Create;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(p => p.Message)
            .NotEmpty()
            .MaximumLength(150);
    }
}
