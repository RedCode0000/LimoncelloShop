using System.ComponentModel.DataAnnotations;

namespace LimoncelloShop.Domain.Objects
{
    public class BasketItemDTO
    {
        public static BasketItemDTO ToBasketItemDTO(BasketItem basketItem)
        {
            return new BasketItemDTO
            {
                Id = basketItem.Id,
                NameOfLimoncello = basketItem.Limoncello.Name,
                //BasketId = basketItem.Basket.Id,
                Email = basketItem.Basket.User != null ? basketItem.Basket.User.Email : null,
                Number = basketItem.Number
            };
        }

        public static BasketItem ToBasketItem(BasketItemDTO itemDTO)
        {
            return new BasketItem
            {
                Id = itemDTO.Id,
                Limoncello = new Limoncello
                {
                    Name = itemDTO.NameOfLimoncello
                },
                Basket = new Basket
                {
                    User = itemDTO.Email != null ? new User { Email = itemDTO.Email } : null,
                    Cookie = itemDTO.Cookie
                },
                Number = itemDTO.Number
            };
        }

        public int Id { get; set; }

        public string? Email { get; set; }

        [Required]
        public string NameOfLimoncello { get; set; }

        //[Required]
        //public int BasketId { get; set; }

        [Required]
        public int Number { get; set; }

        public string? Cookie { get; set; }
    }
}
