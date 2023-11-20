using LimoncelloShop.Domain.Objects;

namespace LimoncelloShop.Domain.Interfaces
{
    public interface IBasketItemService
    {
        BasketItem? GetBasketItem(int id, bool isInRole, string? cookieValue = null, string email = "");
        IQueryable<BasketItem> GetAllBasketItems(string? cookieValue = null, string email = "");
        Task DeleteBasketItem(int id);
        Task EditBasketItem(BasketItem basketItem, BasketItemEditDTO model);
        Task AddBasketItem(BasketItem basketItem);
    }
}
