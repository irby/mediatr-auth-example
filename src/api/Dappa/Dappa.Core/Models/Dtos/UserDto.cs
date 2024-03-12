namespace Dappa.Core.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }

    public static UserDto FromUser(User user)
    {
        return new ()
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}
