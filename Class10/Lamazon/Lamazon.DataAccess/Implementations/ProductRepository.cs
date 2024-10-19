using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public List<Product> GetAll()
        {
           return _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                .ToList();
        }

        public List<Product> GetAllFeaturedProducts()
        {
            return _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .Where(x => x.IsFeatured && x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                .ToList();
        }

        public Product GetById(int id)
        {
            return _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .FirstOrDefault(x => x.Id == id && x.ProductStatusId != (int)ProductStatusEnum.Deleted);
        }

        public PagedResultModel<Product> GetFilteredProducts(int? categoryId, int startIndex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<Product>();

            var productsQuery = _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted);

            result.TotalRecords = productsQuery.Count();

            if(categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.ProductCategoryId == categoryId);
            }
            productsQuery = productsQuery.Where(x => x.Name.Contains(searchValue));
            result.TotalDisplayRecords = productsQuery.Count();

            productsQuery = ProcessProductsByQuery(productsQuery, orderByColumn, isAscending);

            result.Items = productsQuery.Skip(startIndex).Take(count).ToList();

            return result;
        }

        public int Insert(Product product)
        {
            _applicationDbContext.Products.Add(product);
            _applicationDbContext.SaveChanges();
            return product.Id;
        }

        public void Update(Product product)
        {
            if(_applicationDbContext.Products.Any(x => x.Id == product.Id && x.ProductStatusId != (int)ProductStatusEnum.Deleted))
            {
                _applicationDbContext.Update(product);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Product with id {product.Id} was not found.");
            }
        }

        public void DeleteById(int id)
        {
            var product = _applicationDbContext.Products.SingleOrDefault(x => x.Id == id);
            if(product == null)
            {
                throw new Exception($"Product with {id} was not found");
            }
            product.ProductStatusId = (int)ProductStatusEnum.Deleted;
            _applicationDbContext.Update(product);
            _applicationDbContext.SaveChanges();
        }

        private IQueryable<Product> ProcessProductsByQuery(IQueryable<Product> productsQuery, string orderByColumn, bool isAscending)
        {
            productsQuery = orderByColumn switch
            {
                "Name" => isAscending
                        ? productsQuery.OrderBy(x => x.Name)
                        : productsQuery.OrderByDescending(x => x.Name),
                "ProductCategory.Name" => isAscending
                        ? productsQuery.OrderBy(x => x.ProductCategory.Name)
                        : productsQuery.OrderByDescending(x => x.ProductCategory.Name),
                "Price" => isAscending
                        ? productsQuery.OrderBy(x => x.Price)
                        : productsQuery.OrderByDescending(x => x.Price),
                _ => throw new NotImplementedException(),
            };

            return productsQuery;
        }
    }
}
