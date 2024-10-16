using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetAllProducts();
        List<ProductViewModel> GetAllFeaturedProducts();
        ProductViewModel GetProductById(int id);
        PagedResultViewModel<ProductViewModel> GetFilteredProducts(ProductsDatatableRequestViewModel productsDatatableRequestViewModel);
        void CreateProduct(ProductViewModel productViewModel);
        void UpdateProduct(ProductViewModel productViewModel);
        void DeleteProductById(int id);
    }
}
