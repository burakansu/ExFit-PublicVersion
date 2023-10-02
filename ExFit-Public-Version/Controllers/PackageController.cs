using Data.Entities.ExFit;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class PackageController : BaseController<Package>
    {
        public PackageController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
