using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;

namespace LimoncelloShop.Business.Services
{
    public class LimoncelloService : ILimoncelloService
    {
        private readonly IRepository<Limoncello> _repository;
        public LimoncelloService(IRepository<Limoncello> repository)
        {
            _repository = repository;
        }
        public async Task AddLimoncello(Limoncello alcoholDrink)
        {
            await _repository.Create(alcoholDrink);
            await _repository.Save();
        }

        public async Task DeleteLimoncello(int id)
        {
            if (_repository.GetById(id) == null)
                throw new ArgumentException($"Attempted to delete ticket with id {id}, which does not exist.");
            await _repository.Delete(id);
            await _repository.Save();
        }

        public async Task EditLimoncello(Limoncello limoncello, LimoncelloDTO model = null, int numberToRemove = 0)
        {
            if (numberToRemove != 0)
                limoncello.Stock -= numberToRemove;

            if (limoncello.Stock < 0)
                throw new ArgumentOutOfRangeException(nameof(numberToRemove), "Stock is empty! Cannot get lower than 0!");

            if (model != null)
            {
                limoncello.Name = model.Name;
                limoncello.Description = model.Description;
                limoncello.AlcoholPercentage = model.AlcoholPercentage;
                limoncello.Stock = model.Stock;
                limoncello.Price = model.Price;
                limoncello.Volume = model.Volume;
                limoncello.ImageFileName = model.ImageFileName;
            }

            _repository.Update(limoncello);
            await _repository.Save();
        }

        public IQueryable<Limoncello> GetAllLimoncello()
        {
            return _repository.GetAll();
        }

        public Limoncello GetLimoncelloByName(string name)
        {
            return _repository.GetAll().FirstOrDefault(x => x.Name == name);
        }

        public Limoncello? GetLimoncello(int id)
        {
            if (id <= 0)
                throw new ArgumentException($"Attempted to get ticket with id {id}, id cannot be lower than 1.");
            Limoncello? lemoncello = _repository.GetById(id);
            if (lemoncello == null)
                return null;
            return lemoncello;
        }
    }
}
