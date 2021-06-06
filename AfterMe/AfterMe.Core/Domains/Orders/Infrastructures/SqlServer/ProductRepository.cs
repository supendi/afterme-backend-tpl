using AfterMe.Core.Domains.Orders.Entities;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders.Infrastructures.SqlServer
{
    public class ProductRepository : IProductRepository
    {
        OrderDbContext dbContext;
        public ProductRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> Add(Product entity)
        {
            var entry = await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public void Delete(string entityId)
        {
            Product product = dbContext.Products.Find(entityId);
            if (product is not null)
            {
                dbContext.Products.Remove(product);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> FindById(params object[] keys)
        {
            return await dbContext.Products.FindAsync(keys);
        }

        public async Task<Product> Update(Product entity)
        {
            dbContext.Attach(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
