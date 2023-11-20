using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.EntityFrameworkCore;

namespace LimoncelloShop.Business.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbContext _db;
        public Repository(DbContext db)
        {
            _db = db;
        }
        public async Task Create(TEntity entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            _db.Remove((await _db.FindAsync(typeof(TEntity), id))!);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>();
        }

        public TEntity? GetById(int id)
        {
            return _db.Set<TEntity>()
            .FirstOrDefault(x => x.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _db.Update(entity);
        }
    }
}