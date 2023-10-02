namespace Core.Attributes
{
    public class RestrictionAttribute : Attribute
    {
        public RestrictionAttribute(string Description, string Group, string TargetDiv)
        {
            this.Description = Description;
            this.Group = Group;
            this.TargetDiv = TargetDiv;

        }

        public string Description { get; set; }
        public string Group { get; set; }
        public string TargetDiv { get; set; }
    }
}
