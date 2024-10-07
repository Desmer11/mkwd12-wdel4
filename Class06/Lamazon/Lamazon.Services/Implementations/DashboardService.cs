using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public DashboardViewModel GetDashboardData()
        {
            return new DashboardViewModel
            {
                TotalOrders = _dashboardRepository.CountOrders(),
                // TODO for next class
            };
        }
    }
}
