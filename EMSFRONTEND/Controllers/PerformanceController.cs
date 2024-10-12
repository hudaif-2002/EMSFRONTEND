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
            // Ensure manager role
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized();
            }

            // Get the manager ID from session
            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0;
            if (managerId == 0)
            {
                return Unauthorized("Manager ID not found in session.");
            }

            // Fetch performance data from service
            var performances = await _performanceService.GetPerformancesByManager(managerId);

            // Pass the performances to the view
            return View(performances);
        }
        /*
                public async Task<IActionResult> PerformanceView()
                {
                    *//*var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0;
                    if (managerId == null)
                    {
                        return Unauthorized();
                    }*//*

                    var role = HttpContext.Session.GetString("Role");
                    if (role != "Manager")
                    {
                        return Unauthorized();
                    }
                    var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0;
                    var performances = await _performanceService.GetPerformancesByManager(managerId);

                    return View(performances);
                }*/
    }
}