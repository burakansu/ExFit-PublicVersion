using Data.Entities.ExFit;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class AnalyzeRoomController : BaseController<Cost>
    {
        public AnalyzeRoomController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }

        public IActionResult Economy()
        {
            return View();
        }
    }
}
