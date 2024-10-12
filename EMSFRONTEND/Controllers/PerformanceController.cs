using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EMSFRONTEND.Services;
using System.Collections.Generic;
using EMSFRONTEND.Models;

namespace EMSFRONTEND.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly PerformanceService _performanceService;

        public PerformanceController(PerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        public async Task<IActionResult> PerformanceView()
        {
            

            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized();
            }
            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            var performances = await _performanceService.GetPerformancesByManager(managerId);

            return View(performances);
        }
    }
}

