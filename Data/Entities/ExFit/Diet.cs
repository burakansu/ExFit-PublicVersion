using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Diet : BaseEntity
    {
        public int DietId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Food>? Food { get; set; }
        public ICollection<Member>? Member { get; set; }
    }
}
