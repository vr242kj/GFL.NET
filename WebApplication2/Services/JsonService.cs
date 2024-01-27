using Newtonsoft.Json.Linq;
using WebApplication2.Models.Json;
using WebApplication2.Repositories;

namespace WebApplication2.Services
{
    public class JsonService
    {

        private readonly JsonRepository jsonRepository;

        public JsonService(JsonRepository repository)
        {
            jsonRepository = repository;
        }

        public JsonViewModel FindAllJsons()
        {
            var data = jsonRepository.FindAll();

            var rootNodes = BuildTrees(data);

            return new JsonViewModel { RootNodes = rootNodes };   
        }


        static List<JsonModel> BuildTrees(List<JsonModel> nodes)
        {
            var idToNodeMap = nodes.ToDictionary(node => node.Id);

            foreach (var node in nodes.Where(n => n.ParentId.HasValue))
            {
                JsonModel parentNode = idToNodeMap[node.ParentId.Value];
                parentNode.Children.Add(node);
            }

            return nodes.Where(n => !n.ParentId.HasValue).ToList();
        }

        public void AddNewJson(JObject jsonObject)
        {
            List<JsonModel> nodes = new List<JsonModel>();
            ConvertJsonToDatabaseNodes(jsonObject, null, 0, nodes);
            jsonRepository.Save(nodes);

        }

        static void ConvertJsonToDatabaseNodes(JToken token, int? parentId, int depth, List<JsonModel> nodes)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in token.Children<JProperty>())
                {
                    int nodeId = nodes.Count + 1;
                    nodes.Add(new JsonModel
                    {
                        KeyName = property.Name,
                        Value = property.Value.Type == JTokenType.Object || property.Value.Type == JTokenType.Array ? null : property.Value.ToString(),
                        ParentId = parentId
                    });

                    ConvertJsonToDatabaseNodes(property.Value, nodeId, depth + 1, nodes);
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                int index = 1;
                foreach (var item in token.Children())
                {
                    int nodeId = nodes.Count + 1;
                    nodes.Add(new JsonModel
                    {
                        KeyName = $"[{index}]",
                        Value = item.Type == JTokenType.Object || item.Type == JTokenType.Array ? null : item.ToString(),
                        ParentId = parentId
                    });

                    ConvertJsonToDatabaseNodes(item, nodeId, depth + 1, nodes);
                    index++;
                }
            }
        }
    }
}
