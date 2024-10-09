using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    public class ProductCategoriesController : ControllerBase
    {
        private IProductCategoriesService _productCategoriesService;

        public ProductCategoriesController(IProductCategoriesService productCategoriesService)
        {
            PageName = "Product Categories";
            _productCategoriesService = productCategoriesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProductCategories(DatatableRequestViewModel datatableRequest)
        {
            var tableResult = _productCategoriesService.GetPagedResultViewModel(datatableRequest);
            return Json(tableResult.ToTableData());
        }

        public IActionResult Create()
        {
            var productCategoryViewModel = new ProductCategoryViewModel();
            return View(productCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // create product category

                return RedirectToAction("Index");
            }
            return View(productCategoryViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                // Edit logic...
                return View();
            }
            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // Edit product category

                return RedirectToAction("Index");
            }
            return View(productCategoryViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                // Delete logic...
                return View();
            }
            return new EmptyResult();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id.HasValue)
            {
                // delete ...
                return RedirectToAction("Index");
            }
            return new EmptyResult();
        }
    }
}
