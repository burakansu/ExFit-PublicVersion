using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Package : BaseEntity
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public int Price { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
