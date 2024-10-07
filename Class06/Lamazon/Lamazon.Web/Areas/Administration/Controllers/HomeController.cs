using Lamazon.Domain.Constants;
using Lamazon.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            PageName = "Dashboard";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
