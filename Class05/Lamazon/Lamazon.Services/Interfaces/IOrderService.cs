using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderViewModel orderViewModel);
    }
}
