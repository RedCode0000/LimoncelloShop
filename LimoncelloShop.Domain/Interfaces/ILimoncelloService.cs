using LimoncelloShop.Domain.Objects;

namespace LimoncelloShop.Domain.Interfaces
{
    public interface ILimoncelloService
    {
        Limoncello? GetLimoncello(int id);

        Limoncello? GetLimoncelloByName(string name);

        IQueryable<Limoncello> GetAllLimoncello();
        Task DeleteLimoncello(int id);
        Task EditLimoncello(Limoncello limoncello, LimoncelloDTO model = null, int numberToRemove = 0);
        Task AddLimoncello(Limoncello limoncello);
    }
}
