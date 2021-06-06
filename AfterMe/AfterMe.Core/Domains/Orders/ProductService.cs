using AfterMe.Core.Domains.Orders.Entities;
using AfterMe.Core.InternalLib;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders
{
    /// <summary>
    /// Provides the contract of functionalities for working with the product storage
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
    }

    /// <summary>
    /// Provides the business functions of product entity
    /// </summary>
    public class ProductService
    {
        IProductRepository productRepository;
        ProductCategoryService productCategoryService;

        public ProductService(IProductRepository productRepository, ProductCategoryService productCategoryService)
        {
            this.productRepository = productRepository;
            this.productCategoryService = productCategoryService;
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> CreateProduct(Product product)
        {
            ProductCategory productCategory = await productCategoryService.Get(product.ProductCategoryId);
            string notFoundError = string.Format("The product category with Id '{0}' is not found.", product.ProductCategoryId);
            if (productCategory is null)
            {
                throw new NotFoundException(notFoundError);
            }
            Product newProduct = await productRepository.Add(product);
            return newProduct;
        }

        /// <summary>
        /// Returns a product by the specified product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProduct(string productId)
        {
            //I think we dont have to throw an exception if the records is not found, just handle it in the higher level layer
            Product product = await productRepository.FindById(productId);
            return product;
        }
    }
}
