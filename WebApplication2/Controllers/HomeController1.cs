using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    public class HomeController1 : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost]
        public ActionResult ProcessJson(string jsonString)
        {
            // Десеріалізуємо JSON у словник
            Dictionary<string, object> jsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

            // Рекурсивно обробляємо словник для виводу всіх ключів та значень
            ProcessDictionary(jsonDictionary);

            // Ви можете додатково виконати інші дії з отриманими даними

            return Content("JSON оброблено успішно.");
        }

        private void ProcessDictionary(Dictionary<string, object> dictionary, string indent = "")
        {
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{indent}{kvp.Key}:");

                if (kvp.Value is Dictionary<string, object> nestedDictionary)
                {
                    // Рекурсивно обробляємо вкладений словник
                    ProcessDictionary(nestedDictionary, indent + "  ");
                }
                else
                {
                    Console.WriteLine($"{indent}  {kvp.Value}");
                }
            }
        }

    }
}
}
