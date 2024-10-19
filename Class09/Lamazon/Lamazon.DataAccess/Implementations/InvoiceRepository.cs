using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations
{
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _applicationDbContext.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Invoices
                .Include(x => x.InvoiceLineItems)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResultModel<Invoice>> GetFilteredAsync(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<Invoice>();

            var invoicesQuery = _applicationDbContext.Invoices.Include(x => x.User).AsQueryable();
            result.TotalRecords = invoicesQuery.Count();

            invoicesQuery = invoicesQuery.Where(x => x.InvoiceNumber.Contains(searchValue));
            result.TotalDisplayRecords = invoicesQuery.Count();

            invoicesQuery = ProcessInvoicesByQuery(invoicesQuery, orderByColumn, isAscending);

            result.Items = await invoicesQuery.Skip(startIndex).Take(count).ToListAsync();

            return result;
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

        public async Task UpdateAsync(Invoice invoice)
        {
            if(await _applicationDbContext.Invoices.AnyAsync(x => x.Id == invoice.Id))
            {
                _applicationDbContext.Update(invoice);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Invoice with id {invoice.Id} was not found.");
            }
        }

        private IQueryable<Invoice> ProcessInvoicesByQuery(IQueryable<Invoice> invoicesQuery, string orderByColumn, bool isAscending)
        {
            invoicesQuery = orderByColumn switch
            {
                "InvoiceNumber" => isAscending
                        ? invoicesQuery.OrderBy(x => x.InvoiceNumber)
                        : invoicesQuery.OrderByDescending(x => x.InvoiceNumber),
                "User.FullName" => isAscending
                        ? invoicesQuery.OrderBy(x => x.User.FullName)
                        : invoicesQuery.OrderByDescending(x => x.User.FullName),
                "InvoiceStatus" => isAscending
                        ? invoicesQuery.OrderBy(x => x.InvoiceStatus)
                        : invoicesQuery.OrderByDescending(x => x.InvoiceStatus),
                _ => throw new NotImplementedException(),
            };

            return invoicesQuery;
        }
    }
}
