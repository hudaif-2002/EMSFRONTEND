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
         //Performance view of employees under the manager

        public async Task<IActionResult> PerformanceView()
        {
            // Ensure manager role
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized();
            }

            // Get the manager ID from session
            var managerId = HttpContext.Session.GetInt32("SUserId");
            if (managerId == null || managerId == 0)
            {
                return Unauthorized("Manager ID not found in session.");
            }

            try
            {
                // Fetch performance data from the service
                var performances = await _performanceService.GetPerformancesByManager(managerId.Value);

                // Check if no performance data is returned
                if (performances == null || !performances.Any())
                {
                    return View("NoData");  
                }

                // Pass the performances to the view
                return View(performances);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while fetching performance data.");
            }
        }
        //To check the loggedin user's performance
        public async Task<IActionResult> MyPerformance()
        {
            var userId = HttpContext.Session.GetInt32("SUserId");
            if (userId == null || userId == 0)
            {
                return Unauthorized("User ID not found in session.");
            }

            try
            {
                var performance = await _performanceService.GetPerformanceByUser(userId.Value);

                if (performance == null)
                {
                    return View("NoData");
                }

                // Pass the performance to the view
                return View(performance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching performance data.");
            }
        }

    }
}