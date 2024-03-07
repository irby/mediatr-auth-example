using Dappa.Core.Models.Interfaces;

namespace Dappa.Core.Models;

public sealed class Post : Entity
{
    public string? Message { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
