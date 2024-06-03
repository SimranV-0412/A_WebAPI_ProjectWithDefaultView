using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using taskWebtel.Model;

namespace taskWebtel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private string taskString = "task";
        /// <summary>
        /// get call
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTask")]
        public IActionResult Get() 
        {
            var tasks = getTaskSession();
            return Ok(tasks);
        }
        /// <summary>
        /// post call
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost("postTask")]
        public IActionResult Post([FromBody] taskModel task)
        {
            if(taskString == null)
            {
                return BadRequest();
            }
            var tasks = getTaskSession();
            task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            saveTaskSession(tasks);
            return Ok(task);
        }
        private List<taskModel> getTaskSession()
        {
            var tasksString = HttpContext.Session.GetString(taskString);
            if(tasksString == null)
            {
                return new List<taskModel>();
            }
            return JsonConvert.DeserializeObject<List<taskModel>>(tasksString);
        }
        private void saveTaskSession(List<taskModel> tasks)
        {
            var tasksjson = JsonConvert.SerializeObject(tasks);
            HttpContext.Session.SetString(taskString, tasksjson);
        }
    }
}
