using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Company : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int PackageType { get; set; }
        public string? Logo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime RegistrationTime { get; set; }

        public virtual ICollection<Member>? Member { get; set; }
        public virtual ICollection<User>? User { get; set; }
        public virtual ICollection<Cost>? Cost { get; set; }
        public virtual ICollection<Diet>? Diet { get; set; }
        public virtual ICollection<Excersize>? Excersize { get; set; }
        public virtual ICollection<Income>? Income { get; set; }
        public virtual ICollection<Package>? Package { get; set; }
        public virtual ICollection<Task>? Task { get; set; }
    }
}