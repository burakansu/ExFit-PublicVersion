using Data.Entities.ExFit;
using Data.EntityFramework.ExFit;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class ExcersizeRoomController : BaseController<Excersize>
    {
        public ExcersizeRoomController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
            repo = new ExcersizeRepository();
        }

        public IActionResult ExcersizeRoom()
        {
            return View();
        }
        public IActionResult EditExcersize(int id = 0)
        {
            return View(repo.Get(id));
        }
        public IActionResult PracticeList(int id, int day)
        {
            ViewBag.Day = day;
            return PartialView(repo.Get(id));
        }
        public IActionResult PracticeRoom(int id = 0)
        {
            return View(repo.Get(id));
        }
    }
}