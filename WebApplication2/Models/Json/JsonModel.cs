namespace WebApplication2.Models.Json
{
    public class JsonModel
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int? ParentId { get; set; }
        public List<JsonModel> Children { get; set; } = new List<JsonModel>();
    }
}
