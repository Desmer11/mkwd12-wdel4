using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> GetMaxId();
        Task<int> Insert(Order order);
    }
}
