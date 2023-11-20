namespace LimoncelloShop.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        TEntity? GetById(int id);
        Task Create(TEntity entity);
        Task Delete(int id);
        Task Save();
        void Update(TEntity entity);
    }
}
