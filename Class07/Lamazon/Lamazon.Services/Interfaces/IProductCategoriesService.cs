using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IProductCategoriesService
    {
        PagedResultViewModel<ProductCategoryViewModel> GetPagedResultViewModel(DatatableRequestViewModel datatableRequestViewModel);
    }
}
