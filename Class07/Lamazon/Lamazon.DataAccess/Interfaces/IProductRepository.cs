using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> GetAllFeaturedProducts();
        Product GetById(int id);
    }
}
