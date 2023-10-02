using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Cost : BaseEntity
    {
        public int CostId { get; set; }
        public int Rent { get; set; }
        public int Electric { get; set; }
        public int Water { get; set; }
        public int StaffSalaries { get; set; }
        public int Other { get; set; }
        public int WhichMonth { get; set; }
        public DateTime Year { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}