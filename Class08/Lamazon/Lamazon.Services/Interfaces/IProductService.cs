using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetAllProducts();
        List<ProductViewModel> GetAllFeaturedProducts();
        ProductViewModel GetProductById(int id);
    }
}
