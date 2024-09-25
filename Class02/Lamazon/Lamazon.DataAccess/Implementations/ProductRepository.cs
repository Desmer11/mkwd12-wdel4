using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
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
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
