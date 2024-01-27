using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using WebApplication2.Models;
using WebApplication2.Models.Json;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// старий код для тесту коли колючі через крапку
        //public ActionResult IndexX()
        //{
        //    List<string> data = new List<string>
        //{
        //    "keyA.keyB.keyCvalue1",
        //    "keyA.keyB.keyDvalue2",
        //    "keyA.keyDvalue5",
        //    "keyC.keyDvalue4",
        //    "keyEvalue3"
        //};

        //    Dictionary<string, object> hierarchy = new Dictionary<string, object>();

        //    foreach (var item in data)
        //    {
        //        var parts = item.Split('.');
        //        var keys = parts.Take(parts.Length - 1).ToArray();
        //        var value = parts.Last();

        //        AddToHierarchy(hierarchy, keys, value);
        //    }

        //    var viewModel = new HierarchyViewModel
        //    {
        //        Key = "Root",
        //        Children = GenerateViewModelHierarchy(hierarchy)
        //    };

        //    string htmlOutput = GenerateHtml(viewModel);
        //    ViewBag.HtmlOutput = htmlOutput;

        //    return View();
        //}

        //private void AddToHierarchy(Dictionary<string, object> hierarchy, string[] keys, string value)
        //{
        //    Dictionary<string, object> currentLevel = hierarchy;

        //    foreach (var key in keys)
        //    {
        //        if (!currentLevel.ContainsKey(key))
        //        {
        //            currentLevel[key] = new Dictionary<string, object>();
        //        }
        //        currentLevel = (Dictionary<string, object>)currentLevel[key];
        //    }

        //    currentLevel["value"] = value;
        //}

        //private List<HierarchyViewModel> GenerateViewModelHierarchy(Dictionary<string, object> hierarchy)
        //{
        //    return hierarchy.Select(kvp => new HierarchyViewModel
        //    {
        //        Key = kvp.Key,
        //        Value = kvp.Value is Dictionary<string, object> ? null : kvp.Value,
        //        Children = kvp.Value is Dictionary<string, object> ? GenerateViewModelHierarchy((Dictionary<string, object>)kvp.Value) : null
        //    }).ToList();
        //}

        //private string GenerateHtml(HierarchyViewModel hierarchy)
        //{
        //    StringBuilder html = new StringBuilder();
        //    GenerateHtmlRecursively(html, hierarchy, 0);
        //    return html.ToString();
        //}

        //private void GenerateHtmlRecursively(StringBuilder html, HierarchyViewModel node, int indent)
        //{
        //    html.Append($"{new string(' ', indent * 2)}<li>{node.Key}\n");

        //    if (node.Children != null && node.Children.Any())
        //    {
        //        html.Append($"{new string(' ', (indent + 1) * 2)}<ul>\n");
        //        foreach (var child in node.Children)
        //        {
        //            GenerateHtmlRecursively(html, child, indent + 2);
        //        }
        //        html.Append($"{new string(' ', (indent + 1) * 2)}</ul>\n");
        //    }
        //    else if (node.Value != null)
        //    {
        //        html.Append($"{new string(' ', (indent + 1) * 2)}value: {node.Value}\n");
        //    }

        //    html.Append($"{new string(' ', indent * 2)}</li>\n");
        //}

        //public ActionResult ShowTree()
        //{
        //    List<ConfigurationModel> configurations = db.ConfigurationModels.ToList(); // Retrieve configurations from the database

        //    // Organize the configurations into a hierarchical structure
        //    foreach (var configuration in configurations)
        //    {
        //        Console.Write($"{configuration.KeyName}{configuration.Value}");
        //    }


        //    var hierarchicalData = BuildHierarchy(configurations);

        //    return View(hierarchicalData);
        //}

        // закінчення старого коду коли ключі через крапку

        //private Dictionary<string, List<ConfigurationModel>> BuildHierarchy(List<ConfigurationModel> configurations)
        //{
        //    var hierarchicalData = new Dictionary<string, List<ConfigurationModel>>();

        //    foreach (var configuration in configurations)
        //    {
        //        var keys = configuration.KeyName.Split(':');

        //        if (keys.Length > 0)
        //        {
        //            var key = keys[0];
        //            if (!hierarchicalData.ContainsKey(key))
        //            {
        //                hierarchicalData[key] = new List<ConfigurationModel>();
        //            }

        //            hierarchicalData[key].Add(configuration);
        //        }
        //    }

        //    return hierarchicalData;
        //}


        //public ActionResult DisplayTree()
        //{
        //    var flatData = db.ConfigurationModels.ToList();


        //    var treeData = TreeNode.BuildTree(flatData);

        //    return View(treeData);
        //}

        //public IActionResult IndexX()
        //{
        //    List<TreeNode> nodes = RetrieveNodesFromSqlServer();

        //    // Reconstruct the tree structure as needed
        //    Dictionary<string, TreeNode> nodeDictionary = new Dictionary<string, TreeNode>();
        //    foreach (var node in nodes)
        //    {
        //        if (!nodeDictionary.ContainsKey(node.KeyName))
        //        {
        //            nodeDictionary[node.KeyName] = node;
        //        }
        //        else
        //        {
        //            // Handle duplicates or conflicts if needed
        //            // For simplicity, this example assumes no conflicts
        //        }

        //        // Update ParentId for child nodes
        //        if (!string.IsNullOrEmpty(node.ParentId) && nodeDictionary.ContainsKey(node.ParentId))
        //        {
        //            nodeDictionary[node.ParentId].Children.Add(node);
        //        }
        //    }

        //    // Find root nodes (nodes without a parent)
        //    List<TreeNode> rootNodes = new List<TreeNode>();
        //    foreach (var node in nodeDictionary.Values)
        //    {
        //        if (string.IsNullOrEmpty(node.ParentId))
        //        {
        //            rootNodes.Add(node);
        //        }
        //    }



        //    // Pass the root nodes to the view
        //    ConfigurationViewModel viewModel = new ConfigurationViewModel
        //    {
        //        Nodes = rootNodes
        //    };
        //    PrintTree(rootNodes, 0);
        //    // Pass the view model to the view
        //    return View(viewModel);
        //}

        //static void PrintTree(List<TreeNode> nodes, int level)
        //{
        //    foreach (var node in nodes)
        //    {
        //        Console.WriteLine($"{new string(' ', level * 4)}Key: {node.KeyName}, Value: {node.Value}, ParentId: {node.ParentId}");
        //        PrintTree(node.Children, level + 1);
        //    }
        //}

        //static List<TreeNode> RetrieveNodesFromSqlServer()
        //{
        //    List<TreeNode> result = new List<TreeNode>();

        //    string connectionString = "Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;";
        //    string selectQuery = "SELECT KeyName, Value, ParentId FROM Table_1";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand(selectQuery, connection))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string key = reader["KeyName"].ToString();
        //                    string value = reader["Value"] is DBNull ? null : reader["Value"].ToString();
        //                    string parentId = reader["ParentId"] is DBNull ? null : reader["ParentId"].ToString();

        //                    result.Add(new TreeNode(key, value, parentId));
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}




        public IActionResult Index()
        {

            //var data = new List<TreeNode>();


            //using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;"))
            //{
            //    connection.Open();

            //    using (SqlCommand command = new SqlCommand("SELECT KeyName, Value, ParentId FROM Table_5;", connection))
            //    {

            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            int a = 1;
            //            while (reader.Read())
            //            {
            //                TreeNode node = new TreeNode
            //                {
            //                    Id = a++,
            //                    KeyName = reader["KeyName"].ToString(),
            //                    Value = reader["Value"] == DBNull.Value ? null : reader["Value"].ToString(),
            //                    ParentId = reader["ParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ParentId"])
            //                };

            //                data.Add(node);
            //            }
            //        }
            //    }
            //}
            //var rootNodes = BuildTrees(data);
            //foreach (var rootNode in rootNodes)
            //{
            //    PrintTree(rootNode);
            //}

            return View();
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

        static void PrintTree(JsonModel node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.KeyName}{(string.IsNullOrEmpty(node.Value) ? "" : $": {node.Value}")}");

            foreach (var child in node.Children)
            {
                PrintTree(child, indent + "  ");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }



        // новий пост контроллер для treeNode

        [HttpPost]
        public async Task<IActionResult> Action()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    //dynamic jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);

                    List<JsonModel> nodesSent = TransformJsonToTreeNodes(jsonData, null);

                    SaveNodesToSqlServer(nodesSent);
                    // Print the transformed nodes
                    foreach (var node in nodesSent)
                    {
                        Console.WriteLine($"Key: {node.KeyName}, Value: {node.Value}, ParentId: {node.ParentId}");
                    }

                }

                return Json(new { message = "Data received" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }
        }

        static List<JsonModel> TransformJsonToTreeNodes(string json, string parentId)
        {
            List<JsonModel> result = new List<JsonModel>();

            JObject jsonObj = JObject.Parse(json);

            foreach (var property in jsonObj.Properties())
            {
                string key = property.Name;
                JToken value = property.Value;

                if (value is JObject)
                {
                    // Recursively process nested objects
                    //result.Add(new TreeNode(key, null, parentId));
                    //result.AddRange(TransformJsonToTreeNodes(value.ToString(), key));
                }
                else
                {
                    // Add leaf nodes
                    //result.Add(new TreeNode(key, value.ToString(), parentId));
                }
            }

            return result;
        }

        static void SaveNodesToSqlServer(List<JsonModel> nodes)
        {
            //string connectionString = "Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;";
            string connectionString = "Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var node in nodes)
                {
                    string insertQuery = "INSERT INTO Table_1 (KeyName, Value, ParentId) VALUES (@KeyName, @Value, @ParentId)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@KeyName", node.KeyName);
                        command.Parameters.AddWithValue("@Value", node.Value ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ParentId", node.ParentId ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        // стрий пост контроллер коли ключі через крапку
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Action()
        //{
        //    try
        //    {
        //        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //        {
        //            string requestBody = await reader.ReadToEndAsync();
        //            dynamic jsonData = JsonConvert.DeserializeObject<JObject>(requestBody);

        //            List<ConfigurationModel> configurations = ConvertToConfigurationModels(jsonData);

        //            db.ConfigurationModels.AddRange(configurations);
        //            try
        //            {
        //                // Your code that performs database operations
        //                db.SaveChanges();
        //            }
        //            catch (DbUpdateException ex)
        //            {
        //                // Log the exception details
        //                Console.WriteLine($"DbUpdateException: {ex.Message}");

        //                // If you want to access the inner exception, use the following
        //                Exception innerException = ex.InnerException;
        //                while (innerException != null)
        //                {
        //                    Console.WriteLine($"Inner Exception: {innerException.Message}");
        //                    innerException = innerException.InnerException;
        //                }

        //                // Handle or rethrow the exception as needed
        //                throw;
        //            }
        //            catch (Exception ex)
        //            {
        //                // Handle other types of exceptions
        //                Console.WriteLine($"Exception: {ex.Message}");
        //            }

        //            foreach (var configuration in configurations)
        //            {
        //                Console.WriteLine($"KeyName: {configuration.KeyName}, Value: {configuration.Value}");
        //            }
        //        }

        //        return Json(new { message = "Data received" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error processing request: {ex.Message}");
        //    }
        //}

        //private List<ConfigurationModel> ConvertToConfigurationModels(dynamic jsonData)
        //{
        //    List<ConfigurationModel> configurations = new List<ConfigurationModel>();
        //    ProcessJsonObject(jsonData, configurations);
        //    return configurations;
        //}

        //private void ProcessJsonObject(dynamic jsonObject, List<ConfigurationModel> configurations, string currentKey = "")
        //{
        //    foreach (var property in jsonObject)
        //    {
        //        string propertyName = property.Name;
        //        object propertyValue = property.Value;

        //        if (propertyValue is Newtonsoft.Json.Linq.JObject nestedObject)
        //        {
        //            // Рекурсивно обробляємо вкладений об'єкт
        //            ProcessJsonObject(nestedObject, configurations, $"{currentKey}{propertyName}.");
        //        }
        //        else
        //        {
        //            // Додаємо конфігураційний запис до списку
        //            configurations.Add(new ConfigurationModel
        //            {
        //                KeyName = $"{currentKey}{propertyName}",
        //                Value = propertyValue?.ToString()
        //            });
        //        }
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
