using Microsoft.AspNetCore.Mvc;
using Data.Entities.ExFit;
using Core.Attributes;

namespace ExFit.Controllers
{
    public class PersonalsController : BaseController<User>
    {
        public PersonalsController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }

        // Actions
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserSettings(int id)
        {
            return View(repo.Get(id));
        }
        public IActionResult AllActivitiesToday()
        {
            return View();
        }
        public IActionResult AddPersonal()
        {
            return View();
        }
        public IActionResult GetUser(int id)
        {
            return PartialView(repo.Get(id));
        }


        // Task
        [HttpPost]
        public async Task<JsonResult> RegistryingAsync(User form, IFormFile file)
        {
            var loggedInUser = repo.Get(ID);
            form.IMG = await MemberM.UploadFile(file, "wwwroot/Personal");

            int Type = UserM.Save(form);

            switch (Type)
            {
                case 0:
                    await TaskM.Build(form.CompanyId, 4, 0, loggedInUser.UserId);
                    if (form.Company.PackageType > 0)
                    {
                        SmsM.SmsSender(form.Company.Name, $"Hoşgeldin! {form.Name} {DateTime.Now.ToShortDateString()} itibarıyla ExFit Yönetim Paneline Girerek Çalışmaya Başlayabilirsin Mail: {form.Mail} Şifre: {form.Password}", null, new List<User>());
                    }
                    break;

                case 1:
                    await TaskM.Build(form.CompanyId, 9, 0, loggedInUser.UserId);
                    break;

                default:
                    if (form.UserId != loggedInUser.UserId)
                    {
                        await TaskM.Build(form.CompanyId, 10, 0, loggedInUser.UserId);
                    }
                    break;
            }

            return JsonAttribute.Ok;
        }
    }
}