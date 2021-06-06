namespace AfterMe.Core.InternalLib
{
    /// <summary>
    /// The generic interface as the base of repository interfaces
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity GetById(string entityId); 
        TEntity Update(TEntity entity); 
        void Delete(string entityId);
    }
}
