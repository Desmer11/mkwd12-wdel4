using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Enums;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
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
    }
}
