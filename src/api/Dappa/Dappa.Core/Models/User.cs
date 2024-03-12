using Dappa.Core.Models.Enums;
using Dappa.Core.Models.Interfaces;

namespace Dappa.Core.Models;

public class User : Entity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public Role Role { get; set; } = Role.User;
    public ICollection<Post> Posts { get; set; }
}
