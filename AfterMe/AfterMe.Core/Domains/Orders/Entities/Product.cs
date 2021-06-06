namespace AfterMe.Core.Domains.Orders.Entities
{
    /// <summary>
    /// Represents the product entity
    /// </summary>
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductCategoryId { get; set; }
    }

    /// <summary>
    /// Represents the product category entity
    /// </summary>
    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
