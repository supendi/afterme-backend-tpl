using System;
using System.Collections.Generic;

namespace AfterMe.Core.Domains.Orders.Entities
{
    /// <summary>
    /// Represents the Order entity
    /// </summary>
    public class Order
    {
        public string Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    /// <summary>
    /// Represents the order item details
    /// </summary>
    public class OrderItem
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string RowNumber { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }
    }
}
