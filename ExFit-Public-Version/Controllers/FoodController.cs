using Data.Entities.ExFit;

namespace ExFit.Controllers
{
    public class FoodController : BaseController<Food>
    {
        public FoodController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }
    }
}
