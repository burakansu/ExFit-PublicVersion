using Core.Extensions;
using Data.Entities.ExFit;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace ExFit.Controllers
{
    public class LogInController : BaseController<User>
    {
        public LogInController(IConfiguration config, IHttpContextAccessor httpcontext, IWebHostEnvironment webHostEnvironment) : base(config, httpcontext, webHostEnvironment)
        {
        }

        // Actions
        public IActionResult SignIn()
        {
            Set(0);
            return View();
        }
        public IActionResult Register()
        {
            Set(0);
            return View();
        }
        public IActionResult ForgetPassword()
        {
            Set(0);
            return View();
        }



        // Tasks
        [HttpPost]
        public async Task<int> Registering(User user)
        {
            if (UserM.CheckEmail(user.Mail) == 0)
            {
                UserM.Save(user, 1);
                return 1;
            }

            return 0;
        }
        [HttpPost]
        public async Task<int> Entering(User user)
        {
            int id = UserM.Check(user).UserId;
            if (id != 0)
            {
                Set(id);
                UpdateIncomeAuto(id);
                MemberM.PasiveMemberAuto();
            }

            return id;
        }
        private async Task UpdateIncomeAuto(int UserId)
        {
            Income Income = new Income();
            int CompanyId = Db.User.Single(x => x.UserId == UserId).CompanyId;
            foreach (var item in Db.Member.Where(x => x.RegistrationDate >= DateTime.Now.MonthFirstDay() && x.RegistrationDate <= DateTime.Now.MonthLastDay() && x.CompanyId == CompanyId && x.Block == 0).ToList())
            {
                int TotalPrice = (Db.Packages.Where(x => x.PackageId == item.PackageId).Count() > 0)
                ? Db.Packages.Single(x => x.PackageId == item.PackageId).Price + item.Price
                : item.Price;

                Income.Value += TotalPrice;
                Income.CompanyId = CompanyId;
                if (Db.Income.Where(x => x.WhichMonth == DateTime.Now.Month && x.Year == DateTime.Now.Year && x.CompanyId == item.CompanyId).Count() > 0)
                {
                    Income.IncomeId = Db.Income.Single(x => x.WhichMonth == DateTime.Now.Month && x.CompanyId == item.CompanyId && x.Year == DateTime.Now.Year).IncomeId;
                }
                Income.Year = DateTime.Now.Year;
                Income.WhichMonth = DateTime.Now.Month;

                if (Income.IncomeId != 0)
                {
                    Db.Income.Remove(Db.Income.Single(x => x.IncomeId == Income.IncomeId));
                    Db.Income.Add(Income);
                }
                else
                    Db.Income.Add(Income);

                Db.SaveChangesAsync();
            }
        }
        private void Set(int id)
        {
            HttpContext.Session.SetInt32("ID", id);
        }
    }
}