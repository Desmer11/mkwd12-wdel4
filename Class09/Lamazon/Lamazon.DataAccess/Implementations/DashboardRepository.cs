using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Constants;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;

namespace Lamazon.DataAccess.Implementations
{
    public class DashboardRepository : BaseRepository,  IDashboardRepository
    {
        public DashboardRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        #region Users methods
        public int CountCustomers()
        {
            return _applicationDbContext.Users.Count(x => x.RoleKey == Roles.User);
        }

        public List<User> GetAdministrators()
        {
            return _applicationDbContext.Users.Where(x => x.RoleKey == Roles.Admin).ToList();
        }
        #endregion

        #region Products methods
        public int CountActiveProducts()
        {
            return _applicationDbContext.Products.Count(x => x.ProductStatusId == (int)ProductStatusEnum.Active);
        }

        public int CountDeletedProducts()
        {
            return _applicationDbContext.Products.Count(x => x.ProductStatusId == (int)ProductStatusEnum.Deleted);
        }
        #endregion

        #region Orders methods
        public int CountOrders()
        {
            return _applicationDbContext.Orders.Count();
        }

        public int CountAcceptedOrders()
        {
            return _applicationDbContext.Orders.Count(x => x.OrderStatusId == (int)OrderStatusEnum.Accepted);
        }

        public int CountPendingOrders()
        {
            return _applicationDbContext.Orders.Count(x => x.OrderStatusId == (int)OrderStatusEnum.Pending);
        }

        public int CountRejectedOrders()
        {
            return _applicationDbContext.Orders.Count(x => x.OrderStatusId == (int)OrderStatusEnum.Rejected);
        }

        public decimal GetOrdersTotalAmount()
        {
            return _applicationDbContext.Orders
                .Where(x => x.OrderStatusId == (int)OrderStatusEnum.Accepted)
                .Sum(x => x.TotalAmount);
        }
        #endregion

        #region Invoice methods
        public int CountCanceledInvoices()
        {
            return _applicationDbContext.Invoices.Count(x => x.InvoiceStatusId == (int)InvoiceStatusEnum.Canceled);
        }

        public int CountInvoices()
        {
            return _applicationDbContext.Invoices.Count();
        }

        public int CountPaidInvoices()
        {
            return _applicationDbContext.Invoices.Count(x => x.InvoiceStatusId == (int)InvoiceStatusEnum.Paid);
        }

        public int CountPendingInvoices()
        {
            return _applicationDbContext.Invoices.Count(x => x.InvoiceStatusId == (int)InvoiceStatusEnum.PendingPayment);
        }

        public decimal GetInvoicesTotalAmount()
        {
            return _applicationDbContext.Invoices
                .Where(x => x.InvoiceStatusId == (int) InvoiceStatusEnum.Paid)
                .Sum(x => x.TotalAmount);
        }
        #endregion

    }
}
