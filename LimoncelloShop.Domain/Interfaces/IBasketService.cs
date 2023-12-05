using LimoncelloShop.Domain.Objects;

namespace LimoncelloShop.Domain.Interfaces
{
    public interface IBasketService
    {
        Basket? GetBasket(int id);

        Basket? GetBasketByCookie(bool isInRole, string? key = null, string email = "");

        Task EditBasket(BasketItem basketItem);

        Task DeleteBasket(int id);

        Task AddBasket(Basket basket);
    }
}
