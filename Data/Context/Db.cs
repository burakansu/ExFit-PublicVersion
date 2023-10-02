using Microsoft.EntityFrameworkCore;
using Data.Entities.ExFit;
using Task = Data.Entities.ExFit.Task;

namespace Data.Context
{
    public class Db : DbContext
    {
        public Db()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
             options.UseSqlServer("Server=.\\SQLEXPRESS; Database=PortfolioMergedMasterDb; Trusted_Connection=True; TrustServerCertificate=True;");
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<MemberMeazurement> MemberMeazurement { get; set; }
        public DbSet<Diet> Diet { get; set; }
        public DbSet<Excersize> Excersize { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Practice> Practice { get; set; }
        public DbSet<Cost> Cost { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
