using Data.Entities.ExFit;
using Data.EntityFramework.ExFit;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class DietRoomController : BaseController<Diet>
    {
        public DietRoomController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
            repo = new DietRepository();
        }

        public IActionResult DietRoom()
        {
            return View();
        }
        public IActionResult EditDiet(int id = 0)
        {
            return View(repo.Get(id));
        }
        public IActionResult FoodList(int id,int day)
        {
            ViewBag.Day = day;
            return PartialView(repo.Get(id));
        }
        public IActionResult FoodRoom(int id = 0)
        {
            return View(repo.Get(id));
        }
    }
}