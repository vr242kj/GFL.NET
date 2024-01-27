using WebApplication2.Models.Json;

namespace WebApplication2.Repositories
{
    public interface IJsonRepository
    {
        int GetTableRowCount();

        public List<JsonModel> FindAll();
        void Save(List<JsonModel> nodes);

    }
}
