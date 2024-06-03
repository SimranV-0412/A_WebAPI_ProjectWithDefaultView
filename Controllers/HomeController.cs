using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using taskWebtel.Model;

namespace taskWebtel.Controllers
{
    public class HomeController : Controller
    {
        private string taskString = "task";
        public IActionResult Index()
        {
            var tasks = getTaskSession();
            return View(tasks);
        }
        private List<taskModel> getTaskSession()
        {
            var tasksString = HttpContext.Session.GetString(taskString);
            if (tasksString == null)
            {
                return new List<taskModel>();
            }
            return JsonConvert.DeserializeObject<List<taskModel>>(tasksString);
        }
    }
}
