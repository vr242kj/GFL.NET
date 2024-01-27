namespace WebApplication2.Models
{
    public class HierarchyViewModel
    {

        public string Key { get; set; }
        public object Value { get; set; }
        public List<HierarchyViewModel> Children { get; set; }

    }
}
