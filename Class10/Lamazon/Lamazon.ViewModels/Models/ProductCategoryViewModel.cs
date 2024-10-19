using Lamazon.ViewModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lamazon.ViewModels.Models
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductCategoryStatusEnum ProductCategoryStatus { get; set; }
    }
}
