using Dappa.Core.Common.Pipeline;
using FluentValidation;

namespace Dappa.Core.Features.Posts.Get;

public class GetPostQueryValidator : SecureRequestValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
