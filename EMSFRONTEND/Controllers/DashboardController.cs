using Microsoft.AspNetCore.Mvc;

namespace EMSFRONTEND.Controllers
{
    using global::EMSFRONTEND.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace EMSFRONTEND.Controllers
    {
        public class DashboardController : Controller
        {
            private readonly TeamService _teamService;
            public DashboardController(TeamService teamService)
            {
                _teamService = teamService;
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
