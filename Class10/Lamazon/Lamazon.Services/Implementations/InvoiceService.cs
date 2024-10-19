using AutoMapper;
using Lamazon.DataAccess.EntitiesConfig;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
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
        public async Task<List<InvoiceViewModel>> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return _mapper.Map<List<InvoiceViewModel>>(invoices);
        }
        public async Task<InvoiceViewModel> GetInvoiceById(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return _mapper.Map<InvoiceViewModel>(invoice);
        }

        public async Task CreateInvoice(InvoiceViewModel invoice)
        {
            var invoiceDomain = _mapper.Map<Invoice>(invoice);
            int invoiceId = await _invoiceRepository.InsertAsync(invoiceDomain);
            if(invoiceId <= 0)
            {
                throw new Exception("Something went wrong while saving the new invoice");
            }
        }
        public async Task UpdateInvocie(InvoiceViewModel invoice)
        {
            var invoiceDomain = _mapper.Map<Invoice>(invoice);
            await _invoiceRepository.UpdateAsync(invoiceDomain);
        }

        public async Task SetAsPaid(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            invoice.InvoiceStatusId = (int)InvoiceStatusEnum.Paid;
            await _invoiceRepository.UpdateAsync(invoice);
        }
        public async Task CancelInvoice(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            invoice.InvoiceStatusId = (int)InvoiceStatusEnum.Canceled;
            await _invoiceRepository.UpdateAsync(invoice);
        }
    }
}
