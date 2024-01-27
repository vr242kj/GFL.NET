using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class JsonController : Controller
    {
        private readonly JsonService _jsonService;

        public JsonController(JsonService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = _jsonService.FindAllJsons();

            return View(viewModel);
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

                    _jsonService.AddNewJson(jsonObject);
                }

                return Json(new { message = "Data received" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing request: {ex.Message}");
            }
        }
    }
}
