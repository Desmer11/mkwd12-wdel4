using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoriesService _productCategoriesService;


        public ProductsController(IProductService productService, IProductCategoriesService productCategoriesService)
        {
            _productService = productService;
            _productCategoriesService = productCategoriesService;


            PageName = "Products";
        }


        public IActionResult Index()
        {
            SetMetadata();
            return View();
        }

        [HttpPost]
        public JsonResult GetProducts(ProductsDatatableRequestViewModel productsDatatableRequestViewModel)
        {
            var pagedResult = _productService.GetFilteredProducts(productsDatatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        }


        private void SetMetadata(ProductViewModel productViewModel = null)
        {
            var productCategories = _productCategoriesService.GetAllProductCategories();
            ViewBag.ProductCategoryList = new SelectList(productCategories, "Id", "Name", productViewModel?.ProductCategoryId);
        }

    }
}
