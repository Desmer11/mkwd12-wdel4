using AutoMapper;
using Lamazon.DataAccess.EntitiesConfig;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultViewModel<InvoiceViewModel>> GetFilteredInvoices(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel?.search?.value ?? string.Empty;
            var invoicesPagedResult = await _invoiceRepository.GetFilteredAsync(
                                                                datatableRequestViewModel.start,
                                                                datatableRequestViewModel.length,
                                                                searchValue,
                                                                datatableRequestViewModel.sortColumn,
                                                                datatableRequestViewModel.isAscending);
            return _mapper.Map<PagedResultViewModel<InvoiceViewModel>>(invoicesPagedResult);
        }
        public Task<List<InvoiceViewModel>> GetAllInvoices()
        {
            throw new NotImplementedException();
        }
        public Task<InvoiceViewModel> GetInvoiceById(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateInvoice(InvoiceViewModel invoice)
        {
            throw new NotImplementedException();
        }
        public Task UpdateInvocie(InvoiceViewModel invoice)
        {
            throw new NotImplementedException();
        }

        public Task CancelInvoice(int id)
        {
            throw new NotImplementedException();
        }
        public Task SetAsPaid(int id)
        {
            throw new NotImplementedException();
        }
    }
}
