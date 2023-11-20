using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Identity;

namespace LimoncelloShop.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User>? GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
