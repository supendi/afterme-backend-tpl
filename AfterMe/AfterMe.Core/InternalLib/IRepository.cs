using System.Threading.Tasks;

namespace AfterMe.Core.InternalLib
{
    /// <summary>
    /// The generic interface as the base of repository interfaces
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> FindById(params object[] keys);
        Task<TEntity> Update(TEntity entity); 
        void Delete(string entityId);
    }
}
