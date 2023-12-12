using LimoncelloShop.Business.Exceptions;
using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.EntityFrameworkCore;

namespace LimoncelloShop.Business.Services
{
    // Toevoegen basketItem met cookie zonder login of zonder cookie met login
    public class BasketItemService : IBasketItemService
    {
        private readonly IRepository<BasketItem> _repository;
        private readonly ILimoncelloService _limoncelloService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketItemService(IRepository<BasketItem> repository,
            ILimoncelloService limoncelloService, IBasketService basketService,
            IUserService userService)
        {
            _repository = repository;
            _limoncelloService = limoncelloService;
            _basketService = basketService;
            _userService = userService;
        }

        public async Task AddBasketItem(BasketItem basketItem)
        {
            //some checks
            ValidationChecks(basketItem);


            // Check if corresponding basketItem is already in the database.
            if (!_repository.GetAll().Any(x => x.Id == basketItem.Id &&
            x.Limoncello.Name == basketItem.Limoncello.Name && x.Basket.Id == basketItem.Basket.Id))
            {
                //Getting limoncello Item
                var limoncello = _limoncelloService.GetLimoncelloByName(basketItem.Limoncello.Name);

                if (limoncello != null)
                    basketItem.Limoncello = limoncello;

                //Getting basket by name and user

                Basket basket;

                if (basketItem.Basket.Id <= 0)
                    basket = null;
                else
                    basket = _basketService.GetBasket(basketItem.Basket.Id);


                if (basket != null)
                {
                    basketItem.Basket = basket;
                    basketItem.Basket.TotalNumberOfItems += basketItem.Number;

                    //Getting optional user
                    if (basket.User != null)
                    {
                        var user = await _userService.GetUserByEmail(basket.User.Email);
                        basketItem.Basket.User = user;
                    }
                }
                else
                {
                    //Getting basket by cookie
                    var basketFromCookie = _basketService.GetBasketByCookie(false, basketItem.Basket.Cookie);

                    if (basketFromCookie != null)
                    {
                        basketItem.Basket = basketFromCookie;

                        basketItem.Basket.TotalNumberOfItems += basketItem.Number;
                    }
                    else
                    {
                        basketItem.Basket.TotalNumberOfItems = basketItem.Number;
                    }
                }

                // if there is a basket: We have to check if the second BasketItem is not the same as the first one, according to the basket and limoncello name,
                // and ultimately, their own names.
                // if there is no basket found: new basket will be made, so which limoncello will be added doesnt matter

                if (_repository.GetAll().Any(x => x.Basket.Id == basketItem.Basket.Id
                        && x.Limoncello.Name == basketItem.Limoncello.Name))
                {
                    await _limoncelloService.EditLimoncello(basketItem.Limoncello, null, basketItem.Number);
                    var existingBasketItem = GetByName(basketItem.Limoncello.Name);
                    existingBasketItem.Number += basketItem.Number;
                    _repository.Update(existingBasketItem);
                    await _repository.Save();

                    return;
                    //throw new ArgumentException("BasketItem cannot have the same basket and limoncelloItem, or basket cannot have different basketItems with same limoncello");
                }

                if (_repository.GetAll().Any(x => x.Id == basketItem.Id))
                    throw new ArgumentException("BasketItems cannot have the same Id's");

                await _repository.Create(basketItem);
                await _repository.Save();
            }
            else
            {
                throw new ObjectExistsException($"BasketItem already exists in the database.");
            }
        }

        private void ValidationChecks(BasketItem basketItem)
        {
            if (basketItem == null)
            {
                throw new ArgumentNullException(nameof(basketItem));
            }

            if (basketItem.Number < 0)
            {
                throw new ArgumentOutOfRangeException($"Amount of the basketItem {basketItem.Number} is a negative number");
            }
        }

        public async Task DeleteBasketItem(int id)
        {
            var basketItem = _repository.GetById(id);

            if (basketItem == null)
                throw new ArgumentException($"Attempted to delete basketItem with id {id}, which does not exist.");

            await _basketService.EditBasket(basketItem);

            await _repository.Delete(id);
            await _repository.Save();
        }

        public async Task EditBasketItem(BasketItem existingBasketItem, BasketItemEditDTO model)
        {
            if (existingBasketItem.Number + model.Number < 0)
            {
                throw new ArgumentException("Your BasketItem number cannot be negative");
            }

            if (existingBasketItem.Number > model.Number)
            {
                int difference = Math.Abs(existingBasketItem.Number - model.Number);
                existingBasketItem.Number -= difference;
                existingBasketItem.Basket.TotalNumberOfItems -= difference;
            }
            else
            {
                int difference = Math.Abs(existingBasketItem.Number - model.Number);
                existingBasketItem.Number += difference;
                existingBasketItem.Basket.TotalNumberOfItems += difference;
            }

            _repository.Update(existingBasketItem);
            await _repository.Save();
        }

        public IQueryable<BasketItem> GetAllBasketItems(string? cookieValue = null, string email = "")
        {
            var basketItems = _repository.GetAll().Include(x => x.Limoncello).Include(x => x.Basket)
                .ThenInclude(x => x.User);

            if (cookieValue != null)
            {
                return basketItems.Where(x => x.Basket.Cookie == cookieValue);
            }
            else
            {
                return basketItems.Where(x => x.Basket.User.Email == email);
            }
        }

        public BasketItem? GetBasketItem(int id, bool isInRole, string? cookieValue = null, string email = "")
        {
            if (id <= 0)
                throw new ArgumentException($"Attempted to get basketItem with id {id}, id cannot be lower than 1.");

            BasketItem? basketItem = _repository.GetAll().Include(x => x.Limoncello).Include(x => x.Basket).ThenInclude(x => x.User).FirstOrDefault(x => x.Id == id);

            if (basketItem == null || (isInRole && basketItem.Basket.User.Email != email && cookieValue == null))
                return null;

            return basketItem;
        }

        private BasketItem GetByName(string name)

        {
            return _repository.GetAll().Include(x => x.Limoncello).Include(x => x.Basket)
                .ThenInclude(x => x.User).FirstOrDefault(x => x.Limoncello.Name == name)!;
        }
    }
}
