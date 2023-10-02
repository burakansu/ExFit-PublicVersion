using Data.Entities.ExFit;
using Task = Data.Entities.ExFit.Task;

namespace ExFit.Custom
{
    public interface IPageMetaData
    {
        string PageTitle { get; set; }
        string PageDescription { get; set; }
        string[] BodyCssClasses { get; set; }
        public List<BaseMenu> baseMenu(string pre, string nameSpace);
        public string ScriptVersion();
        public string EnvironmentVersion();
        public List<Company> baseCompany();
        public List<Company> baseCompanyFull();
        public IQueryable<Cost> baseCost();
        public IQueryable<Diet> baseDiet();
        public IQueryable<Excersize> baseExcersize();
        public IQueryable<Income> baseIncome();
        public IQueryable<Package> basePackage();
        public IQueryable<Member> baseMember();
        public IQueryable<Task> baseTask();
        public IQueryable<User> baseUser();
        public User currentUser();
        public int Income();
        public int TotalCost();
        public List<Cost> ThisYearCosts();
        public int Profit();
        public int[] ThisYearRegistry();
        public int[] IncomeArray();
        public int[] CostArray();
    }
}
