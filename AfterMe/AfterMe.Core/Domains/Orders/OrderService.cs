using AfterMe.Core.Domains.Orders.Entities;
using AfterMe.Core.InternalLib;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders
{
    /// <summary>
    /// The contracts for working with order storage
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> FindByOrderNumber(string orderNumber);
        Task<int> Count();
    }

    /// <summary>
    /// The order auto numbering contract
    /// </summary>
    public interface IAutoNumberOrder
    {
        string GetNextNumber();
    }

    /// <summary>
    /// Provides the Order business functions 
    /// </summary>
    public class OrderService
    {
        IOrderRepository orderRepository;
        IAutoNumberOrder orderAutoNumbering;
        ProductService productService; 

        public OrderService(IOrderRepository orderRepository, IAutoNumberOrder orderAutoNumbering, ProductService productService)
        {
            this.orderRepository = orderRepository;
            this.orderAutoNumbering = orderAutoNumbering;
            this.productService = productService;
        }

        private void ValidateProducts(Order order)
        {
            //Check if the items(product) are exist
            foreach (OrderItem item in order.OrderItems)
            {
                if (productService.GetProduct(item.Id) == null)
                {
                    throw new NotFoundException(string.Format("Product with id '{0}' is not found", item.Id));
                }
            }
        }

        public async Task<Order> CreateOrder(Order order)
        {
            ValidateProducts(order);

            order.OrderNumber = orderAutoNumbering.GetNextNumber();
            Order newOrder = await orderRepository.Add(order);
            return newOrder;
        }

        public async Task<Order> GetOrder(string orderId)
        { 
            Order order = await orderRepository.FindById(orderId);
            return order;
        }
    }
}
