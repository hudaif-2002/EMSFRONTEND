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

        // GET: /EmpTaskList/AssignTask
        public async Task<IActionResult> AssignTask()
        {
            var employees = await _taskListService.GetEmployeesForManagerAsync();
            return View(employees);
        }

        // POST: /EmpTaskList/AssignTask
        [HttpPost]
        public async Task<IActionResult> AssignTask(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.AssignTaskAsync(model);
                if (isSuccess)
                {
                    return RedirectToAction("TaskList");
                }
                ModelState.AddModelError("", "Failed to assign task.");
            }
            return View(model);
        }

        // GET: /EmpTaskList/TaskList
        public async Task<IActionResult> TaskList()
        {
            var tasks = await _taskListService.GetTasksForEmployeeAsync();
            return View(tasks);
        }

        // GET: /EmpTaskList/UploadTask
        public async Task<IActionResult> UploadTask()
        {
            var tasks = await _taskListService.GetTasksForUploadAsync();
            return View(tasks);
        }

        // POST: /EmpTaskList/UploadTask
        [HttpPost]
        public async Task<IActionResult> UploadTask(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.UploadTaskAsync(model);
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
