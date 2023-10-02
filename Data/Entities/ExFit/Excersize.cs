using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Excersize : BaseEntity
    {
        public int ExcersizeId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Practice>? Practice { get; set; }
        public ICollection<Member>? Member { get; set; }
    }
}
