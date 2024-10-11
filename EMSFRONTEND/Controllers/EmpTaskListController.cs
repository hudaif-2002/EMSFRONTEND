using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var userId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get UserId from session
            var tasks = await _taskListService.GetTasksForEmployee(userId);
            var tuple = new Tuple<IEnumerable<TaskModel>, UploadModel>(tasks, new UploadModel());

            return View(tuple);
        }




        /* [HttpGet]
         public IActionResult UploadTask()
         {
             // Retrieve the current user's UserId from the session
             var currentUserId = HttpContext.Session.GetString("SUserId");

             // If the user is not logged in, redirect to login
             if (string.IsNullOrEmpty(currentUserId))
             {
                 return RedirectToAction("Login", "Account");
             }

             // Attempt to parse UserId (from session) to an integer
             if (!int.TryParse(currentUserId, out int userIdInt))
             {
                 return RedirectToAction("Login", "Account"); // If UserId is not a valid integer, redirect to login
             }

             // Fetch tasks assigned to the current user
             var tasks = await _taskListService.GetTasksForEmployee(userId);


             // Create an Upload instance to pass to the view
             var model = new Upload
             {
                 TaskId = 0, // Default to no specific task selected
                 FilePath = string.Empty // Initialize an empty string for the file path
             };

             // Pass the tasks list as ViewBag
             ViewBag.AssignedTasks = tasks;

             // Pass the model to the view
             return View(model);
         }*/

        // POST: /EmpTaskList/UploadTask
        /*[HttpPost]
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
        }*/



        // POST: /EmpTaskList/UploadTask
        [HttpPost]
        public async Task<IActionResult> UploadTask(UploadModel model)
        {
            // Retrieve UserId from session
            var userId = HttpContext.Session.GetInt32("SUserId");

            if (userId == null)
            {
                ModelState.AddModelError("", "User ID is required.");
                return View(model);
            }

            model.UserId = userId.Value; // Set the UserId from session

            if (ModelState.IsValid)
            {
                var isSuccess = await _taskListService.UploadTask(model);
                if (isSuccess)
                {
                    return RedirectToAction("UploadTask");
                }
                ModelState.AddModelError("", "Failed to upload task.");
            }
            return View(model);
        }



		public async Task<IActionResult> ViewUploads()
		{
			var role = HttpContext.Session.GetString("Role");
			if (role != "Manager")
			{
				return Unauthorized();
			}
			var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get ManagerId from session
			var leaveRequests = await _taskListService.GetUploadsForManager(managerId);

			return View(leaveRequests);
		}


	}
}
