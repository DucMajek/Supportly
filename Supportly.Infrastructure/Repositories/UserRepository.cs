using Supportly.Infrastructure.Models;

namespace Supportly.Infrastructure.Repositories;

public class UserRepository
{
    private readonly SupportlyContext _context;

    public UserRepository(SupportlyContext context)
    {
        _context = context;
    }

    public async Task<User> UserLogin(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(e=>e.Email == email && e.PasswordHash == password);

        if (user != null)
        {
            return user;
        }
        return null;
    }

    public async Task<bool> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }
}