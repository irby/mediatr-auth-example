namespace Dappa.Core.Models.Dtos;

public sealed class PostDto
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
    public string? CreatedBy { get; set; }

    public static PostDto FromPost(Post post)
    {
        return new ()
        {
            Id = post.Id,
            Message = post.Message,
            CreatedBy = post.User.Username,
        };
    }
}
