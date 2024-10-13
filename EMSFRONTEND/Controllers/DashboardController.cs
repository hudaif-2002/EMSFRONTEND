/*using Microsoft.AspNetCore.Mvc;

namespace EMSFRONTEND.Controllers
{
    using global::EMSFRONTEND.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace EMSFRONTEND.Controllers
    {
        public class DashboardController : Controller
        {
            private readonly DashboardService _dashboardService;
            private readonly TeamService _teamService;
            public DashboardController(TeamService teamService, DashboardService dashboardService)
            {
                _teamService = teamService;
                _dashboardService = dashboardService;
            }

            public async Task<IActionResult> AdminView()
            {
                var userId = HttpContext.Session.GetInt32("SUserId");
                if (userId == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to access this page.";
                    return RedirectToAction("Login", "Auth");  // Redirect to login if not logged in
                }
                var users = await _dashboardService.GetAllUsersExceptAdminAsync();
                return View(users);
            }

            [HttpPost]
            public IActionResult Logout()
            {
                // Clear the session or any authentication cookies
                HttpContext.Session.Clear();



                // Redirect to login page
                return RedirectToAction("Login", "Auth"); // "Auth" is the controller that has the Login action
            }


            // Action to delete user
            [HttpPost]
            public async Task<IActionResult> DeleteUser(int userId)
            {
                var result = await _dashboardService.DeleteUserAsync(userId);
                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User deleted successfully.";
                }
                else
                {
                    var errorResponse = await result.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Failed to delete user: {errorResponse}";
                }

                return RedirectToAction("AdminView");
            }
            public async Task<IActionResult> ManagerView()
            {
                int userId = HttpContext.Session.GetInt32("SUserId") ?? 0;
                if (userId == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to access this page.";
                    return RedirectToAction("Login", "Auth"); // Redirect to login if not logged in
                }
                ViewBag.UserId = userId;
                ViewBag.UserName = HttpContext.Session.GetString("Username");
                var user = await _teamService.GetManagerDashboardData(userId);
                return View(user);
            }

            public async Task<IActionResult> EmployeeView()
            {
                int userId = HttpContext.Session.GetInt32("SUserId") ?? 0;
               
                if (userId == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to access this page.";
                    return RedirectToAction("Login", "Auth"); // Redirect to login if not logged in
                }
                ViewBag.UserId = userId;
                ViewBag.UserName = HttpContext.Session.GetString("Username");
                var user = await _teamService.GetEmployeeDashboardData(userId);
                return View(user);
            }

            public IActionResult GetDashboardData()
            {
                ViewBag.UserId = HttpContext.Session.GetInt32("SUserId") ?? 0;
                return View();
            }


        }
    }

}
*/




using global::EMSFRONTEND.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using EMSFRONTEND.Filters;  // Include the filter namespace

namespace EMSFRONTEND.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;
        private readonly TeamService _teamService;

        public DashboardController(TeamService teamService, DashboardService dashboardService)
        {
            _teamService = teamService;
            _dashboardService = dashboardService;
        }

        // Only allow Admins to access this page
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> AdminView()
        {
            var users = await _dashboardService.GetAllUsersExceptAdminAsync();
            return View(users);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        // Only allow Managers to access this page
        [AuthorizeRole("Manager")]
        public async Task<IActionResult> ManagerView()
        {
            int userId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            ViewBag.UserId = userId;
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            var user = await _teamService.GetManagerDashboardData(userId);
            return View(user);
        }

        // Only allow Employees to access this page
        [AuthorizeRole("Employee")]
        public async Task<IActionResult> EmployeeView()
        {
            int userId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            ViewBag.UserId = userId;
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            var user = await _teamService.GetEmployeeDashboardData(userId);
            return View(user);
        }

        public IActionResult GetDashboardData()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            return View();
        }

        [HttpPost]
        [AuthorizeRole("Admin")]  // Allow only Admins to delete users
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _dashboardService.DeleteUserAsync(userId);
            if (result.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                var errorResponse = await result.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to delete user: {errorResponse}";
            }

            return RedirectToAction("AdminView");
        }
    }
}
