
using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSFRONTEND.Controllers
{
    // Controller for managing employee task lists
    public class EmpTaskListController : Controller
    {
        private readonly EmpTaskListService _taskListService;

        // Constructor to inject the task list service
        public EmpTaskListController(EmpTaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        // GET: Retrieve all employees under the current manager
        public async Task<IActionResult> AssignTask()
        {
            var currentUserId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get the current user ID from session
            var employees = await _taskListService.GetEmployeesForManager(currentUserId);

            // Ensure employees list is not null; default to an empty list if null
            ViewBag.Employees = employees?.ToList() ?? new List<UsersModel>();
            return View();
        }

        // POST: Assign a task to an employee
        [HttpPost]
        public async Task<IActionResult> AssignTask(TaskModel model)
        {
            Console.WriteLine(model.Description); // Log the task description for debugging
            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.AssignTask(model);
                if (isSuccess)
                {
                    return RedirectToAction("AssignTask"); // Redirect on success
                }
                ModelState.AddModelError("", "Failed to assign task."); // Add model error if task assignment fails
            }

            // Log validation errors to the console
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage + " in the for loop ");
            }
            return View(model); // Return the model to the view if invalid
        }

        // GET: Retrieve all tasks assigned to the employee
        public async Task<IActionResult> TaskList()
        {
            var userId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get user ID from session
            var tasks = await _taskListService.GetTasksForEmployee(userId);
            return View(tasks); // Pass the tasks to the view
        }

        // GET: Display task lists and the upload form
        public async Task<IActionResult> UploadTask()
        {
            var userId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get user ID from session
            var tasks = await _taskListService.GetTasksForEmployee(userId);
            var tuple = new Tuple<IEnumerable<TaskModel>, UploadModel>(tasks, new UploadModel()); // Combine tasks and a new UploadModel

            return View(tuple); // Pass the tuple to the view
        }

        // POST: Handle task upload form submission
        [HttpPost]
        public async Task<IActionResult> UploadTask(UploadModel model)
        {
            var userId = HttpContext.Session.GetInt32("SUserId");

            if (userId == null)
            {
                ModelState.AddModelError("", "User ID is required."); // Add model error if user ID is not found
                return View(model); // Return the model to the view
            }

            model.UserId = userId.Value; // Set the user ID in the model

            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.UploadTask(model, HttpContext);
                if (isSuccess)
                {
                    return RedirectToAction("UploadTask"); // Redirect on success
                }
                else
                {
                    return RedirectToAction("Error", new { errorMessage = "Task ID does not exist." }); // Redirect to error view on failure
                }
            }
            return View(model); // Return the model to the view if invalid
        }

        // GET: View uploads for all employees under the manager
        public async Task<IActionResult> ViewUploads()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized(); // Return 401 Unauthorized if the role is not Manager
            }

            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get Manager ID from session
            var uploads = await _taskListService.GetUploadsForManager(managerId);

            return View(uploads); // Pass uploads to the view
        }

        // GET: View all tasks of employees under the manager
        public async Task<IActionResult> ViewAllTasks()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized(); // Return 401 Unauthorized if the role is not Manager
            }

            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get Manager ID from session
            var alltasks = await _taskListService.GetTasksUnderManager(managerId);

            return View(alltasks); // Pass all tasks with FullName to the view
        }

        // GET: Display error message when task ID is invalid during upload
        public IActionResult Error(string errorMessage)
        {
            ViewData["ErrorMessage"] = errorMessage; // Set error message in ViewData
            return View(); // Return error view
        }

        // POST: Delete a pending task by the manager
        [HttpPost]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await _taskListService.DeleteTaskAsync(taskId); // Call the service to delete the task
            return RedirectToAction("ViewAllTasks"); // Redirect to the tasks view after deletion
        }
    }
}
