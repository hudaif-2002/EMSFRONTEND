using Microsoft.AspNetCore.Mvc;

namespace EMSFRONTEND.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace EMSFRONTEND.Controllers
    {
        public class DashboardController : Controller
        {
            public IActionResult ManagerDashboard()
            {
                return View(); // Ensure you have a view for the manager dashboard
            }

            public IActionResult EmployeeDashboard()
            {
                return View(); // Ensure you have a view for the employee dashboard
            }


            public IActionResult ManagerView()
            {
                return View();
            }

            public IActionResult EmployeeView()
            {
                return View();
            }
        }
    }

}
