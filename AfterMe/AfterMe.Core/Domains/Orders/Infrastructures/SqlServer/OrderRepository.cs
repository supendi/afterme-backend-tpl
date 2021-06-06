using AfterMe.Core.Domains.Orders.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders.Infrastructures.SqlServer
{
    public class OrderRepository : IOrderRepository
    {
        OrderDbContext dbContext;
        public OrderRepository(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Order> Add(Order entity)
        {
            var orderEntry = await dbContext.Orders.AddAsync(entity);
            foreach (var item in entity.OrderItems)
            {
                await dbContext.OrderItems.AddAsync(item);
            }
            await dbContext.SaveChangesAsync();
            return orderEntry.Entity;
        }

        public Task<int> Count()
        {
            int totalRows = dbContext.Orders.Count();
            return Task.FromResult(totalRows);
        }

        public void Delete(string entityId)
        {
            Order order = dbContext.Orders.Find(entityId);
            if (order is not null)
            {
                dbContext.Orders.Remove(order);
                dbContext.SaveChangesAsync();
            }
        }

        public async Task<Order> FindById(params object[] keys)
        {
            return await dbContext.Orders.FindAsync(keys);
        }

        public Task<Order> FindByOrderNumber(string orderNumber)
        {
            Order order = dbContext.Orders.FirstOrDefault(x => x.OrderNumber == orderNumber);
            return Task.FromResult(order);
        }

        public async Task<Order> Update(Order entity)
        {
            dbContext.Attach(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
