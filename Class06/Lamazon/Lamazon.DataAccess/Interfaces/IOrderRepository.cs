using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> GetMaxIdAsync();
        Task<int> InsertAsync(Order order);
    }
}
