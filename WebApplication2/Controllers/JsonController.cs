using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using WebApplication2.Models;
using WebApplication2.Models.Json;

namespace WebApplication2.Controllers
{
    public class JsonController : Controller
    {
        [HttpGet]
        public IActionResult MyIndex()
        {
            var data = new List<JsonModel>();


            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT KeyName, Value, ParentId FROM Table_5;", connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int a = 1;
                        while (reader.Read())
                        {
                            JsonModel node = new JsonModel
                            {
                                Id = a++,
                                KeyName = reader["KeyName"].ToString(),
                                Value = reader["Value"] == DBNull.Value ? null : reader["Value"].ToString(),
                                ParentId = reader["ParentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ParentId"])
                            };

                            data.Add(node);
                        }
                    }
                }
                //foreach (var node in data)
                //{
                //    Console.WriteLine($"Id: {node.Id}, KeyName: {node.KeyName}, Value: {node.Value}, ParentId: {node.ParentId}");
                //}


                // Transform the data into a hierarchical structure

            }

            var rootNodes = BuildTrees(data);
            foreach (var rootNode in rootNodes)
            {
                PrintTree(rootNode);
            }

            var viewModel = new JsonViewModel
            {
                RootNodes = rootNodes
            };

            return View(viewModel);
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


        [HttpPost]
        public async Task<IActionResult> Action()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string jsonData = await reader.ReadToEndAsync();
                    JObject jsonObject = JObject.Parse(jsonData);

                    // Initialize list to store data for database
                    List<JsonModel> nodes = new List<JsonModel>();

                    // Convert JSON to tree structure and fill the list
                    ConvertJsonToDatabaseNodes(jsonObject, null, 0, nodes);

                    // Save nodes in the database
                    SaveNodesInDatabase(nodes);

                    Console.WriteLine("Data saved in the database.");
                }

                return Json(new { message = "Data received" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }
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

        static void SaveNodesInDatabase(List<JsonModel> nodes)
        {
            // Implement your database connection and insertion logic here
            // Example: Using SqlConnection and SqlCommand
            string connectionString = "Data Source=LAPTOP-CDCD3ABD\\BCDEMO;Initial Catalog=mydb;Integrated Security=true;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string countQuery = "SELECT COUNT(*) FROM Table_5";
                string insertQuery = "INSERT INTO Table_5 (KeyName, Value, ParentId) VALUES (@KeyName, @Value, @ParentId)";

                using (SqlCommand command = new SqlCommand(countQuery, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    foreach (var node in nodes)
                {
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@KeyName", node.KeyName);
                            insertCommand.Parameters.AddWithValue("@Value", node.Value ?? (object)DBNull.Value);
                            insertCommand.Parameters.AddWithValue("@ParentId", rowCount + node.ParentId ?? (object)DBNull.Value);

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        }
}
