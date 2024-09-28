using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Enums;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductViewModel> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public List<ProductViewModel> GetAllFeaturedProducts()
        {
            var featuredProducts = _productRepository.GetAllFeaturedProducts();
            var mappedProducts = featuredProducts.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DiscountPercentage = x.DiscountPercentage,
                ImageUrl = x.ImageUrl,
                Info = $"{x.Id.ToString("000")} - {x.Name} ({x.ProductCategory.Name})",
                IsFeatured = x.IsFeatured,
                IsAddedToCart = false,
                Price = x.Price,
                ProductCategory = new ProductCategoryViewModel
                {
                    Id = x.ProductCategory.Id,
                    Name = x.ProductCategory.Name,
                    ProductCategoryStatus = (ProductCategoryStatusEnum)x.ProductCategory.ProductCategoryStatus.Id
                },
                ProductCategoryId = x.ProductCategory.Id,
                ProductStatus = (ProductStatusEnum)x.ProductStatus.Id
            });

            return mappedProducts.ToList();
        }

        public ProductViewModel GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
