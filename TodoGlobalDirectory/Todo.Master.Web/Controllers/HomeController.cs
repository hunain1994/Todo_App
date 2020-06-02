using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Todo.Master.Services.Dtos;
using Todo.Master.Web.Models;
using Todo.Master.Web.Utility;

namespace Todo.Master.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _baseApiUrl;
        private readonly ApplicationSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _baseApiUrl = _appSettings.webApiUrl;
        }

        public async Task<IActionResult> Index()
        {
            List<TaskListAndDetailViewModel> taskList = new List<TaskListAndDetailViewModel>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(_baseApiUrl + "Todo/getAllTasks"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        taskList = JsonConvert.DeserializeObject<List<TaskListAndDetailViewModel>>(apiResponse);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                _logger.LogError($"Web exception list task: {e.Message} from master");
            }

            return View(taskList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(TaskCreateViewModel model)
        {
            try
            {
                string VPath = _baseApiUrl + "Todo/addNewTask";
                TaskListAndDetailViewModel result = new TaskListAndDetailViewModel();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await httpClient.PostAsync(VPath, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<TaskListAndDetailViewModel>(apiResponse);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                _logger.LogError($"Web exception create task: {e.Message} from master");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTask(int id, TaskUpdateViewModel model)
        {
            try
            {
                string VPath = _baseApiUrl + "Todo/updateTask/" + id.ToString();
                bool result = false;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var response = await httpClient.PutAsync(VPath, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                _logger.LogError($"Web exception update task: {e.Message} from master");
            }
            return RedirectToAction("Index");
        }

        
        
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                string VPath = _baseApiUrl + "Todo/deleteTask/" + id.ToString();
                bool result = false;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(VPath))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                _logger.LogError($"Web exception delete task: {e.Message} from master");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
