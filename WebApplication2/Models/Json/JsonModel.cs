namespace WebApplication2.Models.Json
{
    public class JsonModel
    {

        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public int? ParentId { get; set; }
        public List<JsonModel> Children { get; set; } = new List<JsonModel>();


        //public string KeyName { get; set; }
        //public string Value { get; set; }
        //public string ParentId { get; set; }

        //public List<TreeNode> Children { get; set; }

        //public TreeNode(string key, string value, string parentId)
        //{
        //    KeyName = key;
        //    Value = value;
        //    ParentId = parentId;
        //    Children = new List<TreeNode>();
        //}

        //public TreeNode(string key, string value, string parentId, List<TreeNode> children)
        //{
        //    KeyName = key;
        //    Value = value;
        //    ParentId = parentId;
        //    Children = children;
        //}
        //public string Name { get; set; }
        //public string Value { get; set; }
        //public List<TreeNode> Children { get; set; } = new List<TreeNode>();

        //public static List<TreeNode> BuildTree(List<ConfigurationModel> flatData)
        //{
        //    Dictionary<string, TreeNode> nodeDictionary = new Dictionary<string, TreeNode>();

        //    foreach (var item in flatData)
        //    {
        //        string[] keys = item.KeyName.Split(':');
        //        TreeNode parentNode = null;

        //        for (int i = 0; i < keys.Length; i++)
        //        {
        //            string key = keys[i];

        //            if (!nodeDictionary.TryGetValue(key, out TreeNode node))
        //            {
        //                node = new TreeNode { Name = key };
        //                nodeDictionary[key] = node;

        //                if (parentNode != null)
        //                {
        //                    parentNode.Children.Add(node);
        //                }
        //            }

        //            parentNode = node;
        //        }

        //        parentNode.Value = item.Value;
        //    }

        //    return nodeDictionary.Values.Where(n => n.Name.Contains(":") == false).ToList();
        //}
    }
}
