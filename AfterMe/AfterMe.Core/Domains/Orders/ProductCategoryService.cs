using AfterMe.Core.Domains.Orders.Entities;
using AfterMe.Core.InternalLib;
using System;
using System.Threading.Tasks;

namespace AfterMe.Core.Domains.Orders
{
    /// <summary>
    /// The functionality contracts for working with the product category storage
    /// </summary>
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<ProductCategory> FindByName(string categoryName);
    }

    /// <summary>
    /// Provides the business functions of product category
    /// </summary>
    public class ProductCategoryService
    {
        IProductCategoryRepository productCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategory> Create(ProductCategory productCategory)
        {
            if (productCategory is null)
            {
                throw new ArgumentNullException();
            }
            string duplicateError = string.Format("The product category name '{0}' is already exist.", productCategory.Name);

            bool productNameIsExist = productCategoryRepository.FindByName(productCategory.Name) != null;
            if (productNameIsExist)
            {
                throw new DuplicateRecordException(duplicateError);
            }
            ProductCategory newProductCategory = await productCategoryRepository.Add(productCategory);
            return newProductCategory;
        }

        public async Task<ProductCategory> Get(string categoryId)
        {
            ProductCategory existingProductCategory = await productCategoryRepository.FindById(categoryId);
            return existingProductCategory;
        }
    }
}
