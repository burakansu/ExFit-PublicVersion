using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Task : BaseEntity
    {
        public int TaskId { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
