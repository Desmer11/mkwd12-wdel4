using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;

namespace Lamazon.DataAccess.Implementations
{
    public class ProductCategoriesRepository : BaseRepository, IProductCategoriesRepository
    {
        public ProductCategoriesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public PagedResultModel<ProductCategory> GetFilteredProductCategories(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<ProductCategory>();

            var productCategoriesQuery = _applicationDbContext.ProductCategories
                .Where(x => x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted);

            result.TotalRecords = productCategoriesQuery.Count();

            productCategoriesQuery = productCategoriesQuery.Where(x => x.Name.Contains(searchValue));
            result.TotalDisplayRecords = productCategoriesQuery.Count();

            // TODO : Implement sorting logic

            result.Items = productCategoriesQuery.Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
