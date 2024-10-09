using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductCategoriesRepository
    {
        PagedResultModel<ProductCategory> GetFilteredProductCategories(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending);
    }
}
