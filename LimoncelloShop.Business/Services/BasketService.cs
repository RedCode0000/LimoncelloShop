using LimoncelloShop.Business.Exceptions;
using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.EntityFrameworkCore;

namespace LimoncelloShop.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _repository;
        private readonly IUserService _userService;

        public BasketService(IRepository<Basket> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task AddBasket(Basket basket)
        {
            NullCheckForCreating(basket);

            if (!_repository.GetAll().Any(x => x.Id == basket.Id))
            {
                //Getting user
                var user = await _userService.GetUserByEmail(basket.User.Email)!;

                if (user != null)
                    basket.User = user;


                await _repository.Create(basket);
                await _repository.Save();
            }
            else
            {
                throw new ObjectExistsException($"Basket Id {basket.Id} already exists in the database.");
            }
        }

        private void NullCheckForCreating(Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }
        }

        public async Task DeleteBasket(int id)
        {
            var basket = _repository.GetById(id);

            NullCheckBeforeDeleting(id, basket);

            await _repository.Delete(id);
            await _repository.Save();
        }

        private void NullCheckBeforeDeleting(int id, Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException($"Basket with Id {id} not found.");
            }
        }

        //public async Task EditBasket(Basket basket, BasketDTO model)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<Basket> GetAllBaskets()
        //{
        //    throw new NotImplementedException();
        //}

        public Basket? GetBasket(int id)
        {
            OutOfRangeValidationCheck(id);

            var basket = _repository.GetAll().Include(x => x.User).FirstOrDefault(x => x.Id == id);

            NullCheckWithInt(id, basket);

            return basket;
        }

        private void NullCheckWithInt(int id, Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Brand with id {id} not found.");
            }
        }

        private void OutOfRangeValidationCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
        }

        public Basket? GetBasketByCookie(string key)
        {
            var a = _repository.GetAll().FirstOrDefault(x => x.Cookie == key);
            return a;
        }

        public async Task EditBasket(BasketItem basketItem)
        {
            basketItem.Basket.TotalNumberOfItems -= basketItem.Number;
            _repository.Update(basketItem.Basket);
            await _repository.Save();
        }
    }
}
