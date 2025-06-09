using Supportly.Application.Interfaces;
using Supportly.Infrastructure.Models;
using Supportly.Infrastructure.Repositories;

namespace Supportly.Application.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> UserLogin(string email, string password)
    {
        var userLogin = await _userRepository.UserLogin(email, password);
        return userLogin;
    }

    public async Task<bool> UserRegister(User user)
    {
        var response = await _userRepository.CreateUser(user);
        return response;
    }
}