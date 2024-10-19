using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Enums;
using Lamazon.ViewModels.Models;
using System.Transactions;

namespace Lamazon.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task CreateOrder(OrderViewModel orderViewModel)
        {
            orderViewModel.OrderDate = DateTime.Now;
            orderViewModel.OrderStatus = OrderStatusEnum.Pending;
            int maxId = await _orderRepository.GetMaxIdAsync();
            orderViewModel.OrderNumber = $"{++maxId}/{DateTime.Now.Year}";

            var order = _mapper.Map<Order>(orderViewModel);
            int orderId = await _orderRepository.InsertAsync(order);
            if (orderId <= 0)
            {
                throw new Exception("Something went wrong while saving the new order!");
            }
        }

        public async Task<PagedResultViewModel<OrderViewModel>> GetFilteredOrders(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel.search?.value ?? string.Empty;
            var ordersPagedResult = await _orderRepository.GetFilteredOrdersAsync(
                startIndex: datatableRequestViewModel.start,
                count: datatableRequestViewModel.length,
                searchValue: searchValue,
                orderByColumn: datatableRequestViewModel.sortColumn,
                isAscending: datatableRequestViewModel.isAscending
            );

            return _mapper.Map<PagedResultViewModel<OrderViewModel>>(ordersPagedResult);
        }

        public async Task<OrderViewModel> GetById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task RejectOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            order.OrderStatusId = (int)Domain.Enums.OrderStatusEnum.Rejected;
            await _orderRepository.Update(order);
        }

        public async Task AcceptOrder(int id)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = await _orderRepository.GetByIdAsync(id);
                    order.OrderStatusId = (int)Domain.Enums.OrderStatusEnum.Accepted;
                    await _orderRepository.Update(order);

                    var invoice = _mapper.Map<Invoice>(order);
                    var maxId = await _invoiceRepository.GetMaxIdAsync();
                    invoice.InvoiceNumber = $"{++maxId}/{DateTime.Now.Year}";

                    await _invoiceRepository.InsertAsync(invoice);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Log Error
                    Transaction.Current.Rollback();
                }
            }
        }
    }
}
