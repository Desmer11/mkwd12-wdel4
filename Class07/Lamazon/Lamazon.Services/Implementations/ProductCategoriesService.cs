using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.Implementations
{
    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        private readonly IMapper _mapper;

        public ProductCategoriesService(IProductCategoriesRepository productCategoriesRepository, IMapper mapper)
        {
            _productCategoriesRepository = productCategoriesRepository;
            _mapper = mapper;
        }

        public PagedResultViewModel<ProductCategoryViewModel> GetPagedResultViewModel(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel.search?.value ?? string.Empty;
            var productCategoriesPagedResult = _productCategoriesRepository.GetFilteredProductCategories(
                startIndex: datatableRequestViewModel.start,
                count: datatableRequestViewModel.length,
                searchValue: searchValue,
                orderByColumn: datatableRequestViewModel.sortColumn,
                isAscending: datatableRequestViewModel.isAscending
            );
            var mappedPagedResult = _mapper.Map<PagedResultViewModel<ProductCategoryViewModel>>(productCategoriesPagedResult);
            return mappedPagedResult;
        }
    }
}
