using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> GetMaxIdAsync();
        Task<int> InsertAsync(Order order);
        Task<PagedResultModel<Order>> GetFilteredOrdersAsync(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending);
        Task<Order> GetByIdAsync(int id);
        Task Update(Order order);
    }
}
