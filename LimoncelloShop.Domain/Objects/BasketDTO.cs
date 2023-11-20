using System.ComponentModel.DataAnnotations;

namespace LimoncelloShop.Domain.Objects
{
    public class BasketDTO
    {
        public static BasketDTO ToBasketDTO(Basket basket)
        {
            return new BasketDTO
            {
                Id = basket.Id,
                Email = basket.User!.Email!,
                TotalNumberOfItems = basket.TotalNumberOfItems
            };
        }

        public static Basket ToBasket(BasketDTO basketDTO)
        {
            return new Basket
            {
                Id = basketDTO.Id,
                User = new User { Email = basketDTO.Email },
                TotalNumberOfItems = basketDTO.TotalNumberOfItems
            };
        }

        public int Id { get; set; }

        public string? Email { get; set; }

        [Required]
        public int TotalNumberOfItems { get; set; }

        public string Cookie { get; set; } = "";
    }
}

