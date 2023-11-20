using LimoncelloShop.Domain.Objects;

namespace LimoncelloShop.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User>? GetUserByEmail(string email);
    }
}
