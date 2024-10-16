using AutoMapper;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.ProductStatus, opt => opt.Ignore())
                .ForMember(x => x.Info, opt => opt.MapFrom(s => $"{s.Id.ToString("000")} - {s.Name} ({s.ProductCategory.Name})"))
                .ForMember(x => x.ProductStatus, opt => opt.MapFrom(s => s.ProductCategoryId))
                .ReverseMap()
                .ForMember(x => x.ProductStatus, opt => opt.Ignore())
                .ForMember(x => x.ProductStatusId, opt => opt.MapFrom(x => x.ProductStatus));

            CreateMap<PagedResultModel<Product>, PagedResultViewModel<ProductViewModel>>().ReverseMap();
        }
    }
}
