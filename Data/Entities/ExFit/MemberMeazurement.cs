using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class MemberMeazurement : BaseEntity
    {
        public int MemberMeazurementId { get; set; }
        public int Shoulder { get; set; }
        public int Chest { get; set; }
        public int Arm { get; set; }
        public int Leg { get; set; }
        public int Belly { get; set; }
        public int Weight { get; set; }
        public int Size { get; set; }
        public int Age { get; set; }
        public int AvarageAsleepTime { get; set; }
        public int AvarageCalorieIntake { get; set; }
        public int WhichMonth { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
