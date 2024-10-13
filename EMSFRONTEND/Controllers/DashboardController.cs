using Microsoft.AspNetCore.Mvc;

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
                ViewBag.UserId = userId;
                ViewBag.UserName = HttpContext.Session.GetString("Username");
                var user = await _teamService.GetManagerDashboardData(userId);
                return View(user);
            }

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


        }
    }

}
