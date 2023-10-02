namespace Core.Attributes
{
    public class MenuAttribute : Attribute
    {
        public MenuAttribute(string Title, string Icon, string Parent, int Order, int ParentOrder)
        {
            this.Parent = Parent;
            this.Title = Title;
            this.Icon = Icon;
            this.Order = Order;
            this.ParentOrder = ParentOrder;
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public string Parent { get; set; }
        public int Order { get; set; }
        public int ParentOrder { get; set; }
    }
}
