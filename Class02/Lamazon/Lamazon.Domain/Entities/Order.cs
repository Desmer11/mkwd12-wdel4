namespace Lamazon.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public string? IpAddress { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryFlagUrl { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public Order()
        {
            Invoices = new HashSet<Invoice>();
        }
    }
}
