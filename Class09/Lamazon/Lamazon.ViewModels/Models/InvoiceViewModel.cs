using Lamazon.ViewModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lamazon.ViewModels.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public OrderViewModel Order { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public UserViewModel User { get; set; }
        [Display(Name = "Invoice Status")]
        public InvoiceStatusEnum InvoiceStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string InvoiceStatusString 
        { 
            get { return InvoiceStatus.ToString(); } 
        }
        public List<InvoiceLineItemViewModel> InvoiceLineItems { get; set; }
    }
}
