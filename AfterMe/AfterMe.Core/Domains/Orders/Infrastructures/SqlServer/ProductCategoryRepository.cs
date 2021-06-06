using AfterMe.Core.Domains.Orders.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders.Infrastructures.SqlServer
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        OrderDbContext dbContext;
        public ProductCategoryRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ProductCategory> Add(ProductCategory entity)
        {
            var entry = await dbContext.ProductCategories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public void Delete(string entityId)
        {
            ProductCategory productCategory = dbContext.ProductCategories.Find(entityId);
            if (productCategory is not null)
            {
                dbContext.ProductCategories.Remove(productCategory);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task<ProductCategory> FindById(params object[] keys)
        {
            return await dbContext.ProductCategories.FindAsync(keys);
        }

        public Task<ProductCategory> FindByName(string categoryName)
        {
            ProductCategory productCategory = dbContext.ProductCategories.FirstOrDefault(x => x.Name == categoryName);
            return Task.FromResult(productCategory);
        }

        public async Task<ProductCategory> Update(ProductCategory entity)
        {
            dbContext.Attach(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
