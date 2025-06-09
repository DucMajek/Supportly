using Supportly.Domain.Enums;

namespace Supportly.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsDeleted { get; set; }
    public Role Role { get; set; }
}