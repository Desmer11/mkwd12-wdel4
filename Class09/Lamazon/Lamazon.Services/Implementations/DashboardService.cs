using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
        }

        public DashboardViewModel GetDashboardData()
        {
            var administrators = _dashboardRepository.GetAdministrators();
            return new DashboardViewModel
            {
                TotalCustomers = _dashboardRepository.CountCustomers(),

                TotalOrders = _dashboardRepository.CountOrders(),
                TotalOrdersAmount = _dashboardRepository.GetOrdersTotalAmount(),
                TotalPendingOrders = _dashboardRepository.CountPendingOrders(),
                TotalAcceptedOrders = _dashboardRepository.CountAcceptedOrders(),
                TotalRejectedOrders = _dashboardRepository.CountRejectedOrders(),

                TotalInvoices = _dashboardRepository.CountInvoices(),
                TotalInvoicesAmount = _dashboardRepository.GetInvoicesTotalAmount(),
                TotalPendingInvoices = _dashboardRepository.CountPendingInvoices(),
                TotalPaidInvoices = _dashboardRepository.CountPaidInvoices(),
                TotalCanceledInvoices = _dashboardRepository.CountCanceledInvoices(),

                TotalDeletedProducts = _dashboardRepository.CountDeletedProducts(),
                TotalActiveProducts = _dashboardRepository.CountActiveProducts(),

                Administrators = _mapper.Map<List<UserViewModel>>(administrators)
            };
        }
    }
}
