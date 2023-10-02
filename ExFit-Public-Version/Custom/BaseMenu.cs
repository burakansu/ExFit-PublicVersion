namespace ExFit.Custom
{
    public class BaseMenu
    {
        public string Parent { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public int ParentOrder { get; internal set; }
    }
}
