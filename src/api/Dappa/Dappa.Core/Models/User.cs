using Dappa.Core.Models.Interfaces;

namespace Dappa.Core.Models;

public sealed class User : Entity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
