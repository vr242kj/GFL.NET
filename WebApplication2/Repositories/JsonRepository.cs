using Microsoft.Data.SqlClient;
using WebApplication2.Models.Json;

namespace WebApplication2.Repositories
{
    public class JsonRepository : IJsonRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public JsonRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int GetTableRowCount()
        {
            using (SqlConnection connection = _dbContext.GetConnection())
            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM {TableName.Json}", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        public List<JsonModel> FindAll()
        {
            var data = new List<JsonModel>();

            using (SqlConnection connection = _dbContext.GetConnection())
            {
                using (SqlCommand command = new SqlCommand($"SELECT KeyName, Value, ParentId FROM {TableName.Json};", connection))
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
            }
            return data;
        }

        public void Save(List<JsonModel> nodes)
        {
            using (SqlConnection connection = _dbContext.GetConnection())
            {
                string insertQuery = $"INSERT INTO {TableName.Json} (KeyName, Value, ParentId) VALUES (@KeyName, @Value, @ParentId)";


                int rowCount = GetTableRowCount();

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
