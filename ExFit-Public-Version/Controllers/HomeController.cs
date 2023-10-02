using Microsoft.AspNetCore.Mvc;
using Task = Data.Entities.ExFit.Task;

namespace ExFit.Controllers
{
    public class HomeController : BaseController<Task>
    {
        public HomeController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult _Activities(int id)
        {
            return PartialView(repo.Get(id));
        }
        public IActionResult _LastTasks()
        {
            return PartialView();
        }
        public IActionResult _Growth()
        {
            return PartialView();
        }
    }
}
