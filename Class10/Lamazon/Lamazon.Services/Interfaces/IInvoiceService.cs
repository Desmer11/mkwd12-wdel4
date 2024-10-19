using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceViewModel>> GetAllInvoices();
        Task<PagedResultViewModel<InvoiceViewModel>> GetFilteredInvoices(DatatableRequestViewModel datatableRequestViewModel);
        Task<InvoiceViewModel> GetInvoiceById(int id);
        Task CreateInvoice(InvoiceViewModel invoice);
        Task UpdateInvocie(InvoiceViewModel invoice);
        Task CancelInvoice(int id);
        Task SetAsPaid(int id);
    }
}
