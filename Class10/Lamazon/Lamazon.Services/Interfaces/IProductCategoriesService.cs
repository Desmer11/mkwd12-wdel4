using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IProductCategoriesService
    {
        List<ProductCategoryViewModel> GetAllProductCategories();
        PagedResultViewModel<ProductCategoryViewModel> GetPagedResultViewModel(DatatableRequestViewModel datatableRequestViewModel);
        void CreateProductCategory(ProductCategoryViewModel productCategoryViewModel);
        void UpdateProductCategory(ProductCategoryViewModel productCategoryViewModel);
        void DeleteProductCategoryById(int id);
        ProductCategoryViewModel GetProductCategoryById(int id);    
    }
}
