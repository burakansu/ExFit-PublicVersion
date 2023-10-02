using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Practice : BaseEntity
    {
        public int PracticeId { get; set; }
        public int Day { get; set; }
        public int BodySection { get; set; }
        public string Name { get; set; }
        public int SetCount { get; set; }
        public int Repeat { get; set; }
        public int CoolDownTime { get; set; }
        public string? Note { get; set; }

        public int ExcersizeId { get; set; }
        public Excersize Excersize { get; set; }
    }
}
