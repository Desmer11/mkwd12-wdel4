using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetInvoices(DatatableRequestViewModel datatableRequestViewModel)
        {
            var pagedResult = await _invoiceService.GetFilteredInvoices(datatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        } 

        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var invoiceViewModel = await _invoiceService.GetInvoiceById(id.Value);
                return View(invoiceViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetAsPaid(int? id)
        {
            if(id.HasValue)
            {
                await _invoiceService.SetAsPaid(id.Value);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelInvoice(int? id)
        {
            if (id.HasValue)
            {
                await _invoiceService.CancelInvoice(id.Value);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
