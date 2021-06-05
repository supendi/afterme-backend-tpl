namespace AfterMe.Core.InternalLib
{
    /// <summary>
    /// The generic interface as the base of repository interfaces
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        TEntity GetById(int entityId); 
        TEntity Update(TEntity entity); 
        void Delete(int entityId);
    }
}
