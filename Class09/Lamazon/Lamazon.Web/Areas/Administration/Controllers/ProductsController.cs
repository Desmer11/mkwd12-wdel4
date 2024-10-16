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


        public IActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            SetMetadata(productViewModel);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(productViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetMetadata(productViewModel);
                return View(productViewModel);
            }
        }


        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var productViewModel = _productService.GetProductById(id.Value);
                SetMetadata(productViewModel);
                return View(productViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(productViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetMetadata(productViewModel);
                return View(productViewModel);
            }
        }


        private void SetMetadata(ProductViewModel productViewModel = null)
        {
            var productCategories = _productCategoriesService.GetAllProductCategories();
            ViewBag.ProductCategoryList = new SelectList(productCategories, "Id", "Name", productViewModel?.ProductCategoryId);
        }

    }
}
