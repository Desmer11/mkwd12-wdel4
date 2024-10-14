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

            if (orderByColumn == "Name")
            {
                productCategoriesQuery = isAscending
                    ? productCategoriesQuery.OrderBy(x => x.Name)
                    : productCategoriesQuery.OrderByDescending(x => x.Name);
            }

            result.Items = productCategoriesQuery.Skip(startIndex).Take(count).ToList();

            return result;
        }

        public ProductCategory GetById(int id)
        {
            return _applicationDbContext.ProductCategories
                .FirstOrDefault(x => x.Id == id && x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted)!;
        }

        public int Insert(ProductCategory productCategory)
        {
            _applicationDbContext.ProductCategories.Add(productCategory);
            _applicationDbContext.SaveChanges();
            return productCategory.Id;
        }

        public void Update(ProductCategory productCategory)
        {
            if (!_applicationDbContext.ProductCategories.Any(x => x.Id == productCategory.Id && x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted))
            {
                throw new Exception($"ProductCategory with id {productCategory.Id} was not found!");
            }

            _applicationDbContext.ProductCategories.Update(productCategory);
            _applicationDbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var productCategory = _applicationDbContext.ProductCategories.FirstOrDefault(x => x.Id == id);
            if (productCategory is null)
            {
                throw new Exception($"Product Category with id {id} does not exist");
            }

            // Performing Soft delete 
            productCategory.ProductCategoryStatusId = (int)ProductCategoryStatusEnum.Deleted;
            _applicationDbContext.Update(productCategory);
            _applicationDbContext.SaveChanges();
        }

    }
}
