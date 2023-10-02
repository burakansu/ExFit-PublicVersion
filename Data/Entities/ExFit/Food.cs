using Data.Entities.Base;

namespace Data.Entities.ExFit
{
    public class Food : BaseEntity
    {
        public int FoodId { get; set; }
        public int MealType { get; set; }
        public string Name { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbonhidrat { get; set; }
        public string? Note { get; set; }
        public int Day { get; set; }

        public int DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
