using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Income : BaseEntity
    {
        public int IncomeId { get; set; }
        public int Value { get; set; }
        public int WhichMonth { get; set; }
        public int Year { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
