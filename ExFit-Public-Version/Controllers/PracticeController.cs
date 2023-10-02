using Data.Entities.ExFit;

namespace ExFit.Controllers
{
    public class PracticeController : BaseController<Practice>
    {
        public PracticeController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }
    }
}