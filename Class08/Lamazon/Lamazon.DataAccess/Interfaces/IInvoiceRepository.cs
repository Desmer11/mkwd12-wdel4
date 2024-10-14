using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<int> GetMaxIdAsync();
        Task<int> InsertAsync(Invoice invoice);
    }
}
