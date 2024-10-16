using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations
{
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<int> GetMaxIdAsync()
        {
            if (await _applicationDbContext.Invoices.AnyAsync())
            {
                return _applicationDbContext.Invoices.Max(x => x.Id);
            }
            return 0;
        }

        public async Task<int> InsertAsync(Invoice invoice)
        {
            await _applicationDbContext.Invoices.AddAsync(invoice);
            await _applicationDbContext.SaveChangesAsync();
            return invoice.Id;
        }
    }
}
