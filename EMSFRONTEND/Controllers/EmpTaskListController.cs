using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EMSFRONTEND.Controllers
{
    public class EmpTaskListController : Controller
    {
        private readonly EmpTaskListService _taskListService;

        public EmpTaskListController(EmpTaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        // GET: /EmpTaskList/GetEmployees
        public async Task<IActionResult> AssignTask()
        {
            var currentUserId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            var employees = await _taskListService.GetEmployeesForManager(currentUserId);

            // Ensure employees is not null
            ViewBag.Employees = employees?.ToList() ?? new List<UsersModel>(); // Default to an empty list if null
            return View();
        }

        //public async Task<IActionResult> AssignTask()
        //{
        //    var currentUserId = HttpContext.Session.GetInt32("SUserId") ?? 0;
        //    var employees = await _taskListService.GetEmployeesForManager(currentUserId);

        //    // Ensure employees is not null
        //    ViewBag.Employees = employees ?? new List<UsersModel>(); // Default to an empty list if null
        //    return View();
        //}


        /*        public async Task<IActionResult> AssignTask()
                {
                    var currentUserId = HttpContext.Session.GetInt32("SUserId") ?? 0;
                    var employees = await _taskListService.GetEmployeesForManager(currentUserId);
                    ViewBag.Employees = employees;
                    return View();
                }*/

        // POST: /EmpTaskList/AssignTask
        [HttpPost]
        public async Task<IActionResult> AssignTask(TaskModel model)
        {
            Console.WriteLine(model.Description);
            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.AssignTask(model);
                if (isSuccess)
                {
                    return RedirectToAction("AssignTask");
                }
                ModelState.AddModelError("", "Failed to assign task.");
            }
            // Log the errors to the console or a logger
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage + " in the for loop ");
            }
            return View(model);
        }

        // GET: /EmpTaskList/TaskList
        
        public async Task<IActionResult> TaskList()
        {
            var userId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get UserId from session
            var tasks = await _taskListService.GetTasksForEmployee(userId);
            return View(tasks);
        }


        // GET: /EmpTaskList/UploadTask
        public async Task<IActionResult> UploadTask()
        {
            var tasks = await _taskListService.GetTasksForUpload();
            return View(tasks);
        }

        // POST: /EmpTaskList/UploadTask
        [HttpPost]
        public async Task<IActionResult> UploadTask(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.UploadTask(model);
                if (isSuccess)
                {
                    return RedirectToAction("TaskList");
                }
                ModelState.AddModelError("", "Failed to upload task.");
            }
            return View(model);
        }
    }
}
