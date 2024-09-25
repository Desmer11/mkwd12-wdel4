using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamazon.DataAccess.EntitiesConfig
{
    public class ProductCategoryStatusConfig : IEntityTypeConfiguration<ProductCategoryStatus>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryStatus> builder)
        {
            
        }
    }
}
