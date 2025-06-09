using Supportly.Infrastructure.Models;
namespace Supportly.Application.Interfaces;

public interface IUserService
{
    Task<User> UserLogin(string email, string password);
    Task<bool> UserRegister(User user);
}