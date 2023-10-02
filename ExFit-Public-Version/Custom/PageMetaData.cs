
using Microsoft.AspNetCore.Mvc;
using Data.Context;
using System.Reflection;
using Data.Entities.ExFit;
using Task = Data.Entities.ExFit.Task;
using Microsoft.EntityFrameworkCore;

namespace ExFit.Custom
{
    public class PageMetaData : IPageMetaData
    {
        private Db _db { get; set; }
        public string PageTitle { get; set; }
        public string PageDescription { get; set; }
        public string[] BodyCssClasses { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PageMetaData(IHttpContextAccessor httpContextAccessor)
        {
            _db = new Db();
            _httpContextAccessor = httpContextAccessor;
        }

        public List<BaseMenu> baseMenu(string pre = "Menu", string nameSpace = "ExFit.Controllers")
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace == nameSpace)
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new
                    {
                        Controller = x.DeclaringType.Name,
                        Action = x.Name,
                        ReturnType = x.ReturnType.Name,
                        Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
                        Attribute = x.GetCustomAttributes().FirstOrDefault(f => f.GetType().Name == pre + "Attribute"),
                        NameSpace = nameSpace
                    })
                    .Where(x => x.Attributes.Contains(pre))
                    .Select(x => new BaseMenu
                    {
                        Parent = x.Attribute.GetType().GetProperty("Parent")?.GetValue(x.Attribute, null).ToString(),
                        Title = x.Attribute.GetType().GetProperty("Title")?.GetValue(x.Attribute, null).ToString(),
                        Icon = x.Attribute.GetType().GetProperty("Icon")?.GetValue(x.Attribute, null).ToString(),
                        Order = Convert.ToInt32(x.Attribute.GetType().GetProperty("tybat")?.GetValue(x.Attribute, null).ToString()),
                        ParentOrder = Convert.ToInt32(x.Attribute.GetType().GetProperty("ParentOrder")?.GetValue(x.Attribute, null).ToString()),
                        Url = "/" + x.Controller.Replace("Controller", "") + "/" + x.Action,
                    })
                    .OrderBy(x => x.ParentOrder)
                    .ThenBy(x => x.Order)
                    .ToList();
        }
        public string EnvironmentVersion()
        {
            throw new NotImplementedException();
        }
        public string ScriptVersion()
        {
            throw new NotImplementedException();
        }
        public List<Company> baseCompany()
        {
            return _db.Company
                .Where(x => !x.Deleted)
                .ToList();
        }
        public List<Company> baseCompanyFull()
        {
            return _db.Company
                .Include(x => x.Diet)
                .Include(x => x.Excersize)
                .Include(x => x.Package)
                .Include(x => x.Member)
                .Include(x => x.Cost)
                .Include(x => x.Income)
                .Include(x => x.Task)
                .Include(x => x.User)
                .Where(x => !x.Deleted)
                .ToList();
        }
        public IQueryable<Cost> baseCost()
        {
            return _db.Cost
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Diet> baseDiet()
        {
            return _db.Diet
                .Include(x => x.Food)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Excersize> baseExcersize()
        {
            return _db.Excersize
                .Include(x => x.Practice)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Income> baseIncome()
        {
            return _db.Income
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Package> basePackage()
        {
            return _db.Packages
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Member> baseMember()
        {
            return _db.Member
                .Include(x => x.Diet)
                .Include(x => x.Excersize)
                .Include(x => x.Package)
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<Task> baseTask()
        {
            return _db.Task
                .Include(x => x.Member)
                .Include(x => x.User)
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public IQueryable<User> baseUser()
        {
            return _db.User
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .AsQueryable();
        }
        public User currentUser()
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            return _db.User.Find((int)context.Session.GetInt32("ID"));
        }

        public int Income()
        {
            return _db.Income
                .Where(x => x.CompanyId == currentUser().CompanyId && x.Year == DateTime.Now.Year)
                .Sum(x => x.Value);
        }
        public int TotalCost()
        {
            return _db.Cost
                .Where(x => x.CompanyId == currentUser().CompanyId).Sum(x => x.Rent) 
                + _db.Cost.Where(x => x.CompanyId == currentUser().CompanyId)
                .Sum(x => x.Electric) 
                + _db.Cost.Where(x => x.CompanyId == currentUser().CompanyId)
                .Sum(x => x.Water) 
                + _db.Cost.Where(x => x.CompanyId == currentUser().CompanyId)
                .Sum(x => x.StaffSalaries) 
                + _db.Cost.Where(x => x.CompanyId == currentUser().CompanyId)
                .Sum(x => x.Other);
        }
        public List<Cost> ThisYearCosts()
        {
            return _db.Cost
                .Where(x => x.CompanyId == currentUser().CompanyId && x.Year.Year == DateTime.Now.Year)
                .OrderBy(x => x.Year)
                .ToList();
        }
        public int Profit()
        {
            return Income() - TotalCost();
        }
        public int[] ThisYearRegistry()
        {
            int[] Months = new int[12];
            var Members = _db.Member.Where(x => x.RegistrationDate.Year == DateTime.Now.Year && x.CompanyId == currentUser().CompanyId);

            foreach (var item in Members)
            {
                Months[item.RegistrationDate.Month - 1] += 1;
            }

            return Months;
        }
        public int[] IncomeArray()
        {
            int[] incomes = new int[baseIncome().Count()];
            int i = 0;

            foreach (var item in baseIncome().ToList())
            {
                incomes[i] = item.Value;
                i++;
            }

            return (incomes.Length > 0) ? incomes : new int[11];
        }
        public int[] CostArray()
        {
            int[] costs = new int[ThisYearCosts().Count()];
            int j = 0;

            foreach (var item in ThisYearCosts().ToList())
            {
                costs[j] = item.CreateDate.Value.Month;
                j++;
            }

            return (costs.Length > 0) ? costs : new int[11];
        }
    }
}
