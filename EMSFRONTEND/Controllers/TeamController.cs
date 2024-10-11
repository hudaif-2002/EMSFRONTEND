using Microsoft.AspNetCore.Mvc;
using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EMSFRONTEND.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        // Action to view employees under a manager
        public async Task<IActionResult> Teams()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized();
            }
            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get ManagerId from session

            // Fetch employees under the given manager

            var employees = await _teamService.GetEmployeesUnderManager(managerId);

            return View(employees);
        }


        public async Task<IActionResult> ProfileView(int userId)
        {
            var userProfile = await _teamService.GetProfileAsync(userId);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: Profile/EditProfile
        [HttpPost]
        public async Task<IActionResult> EditProfile(UsersModel updatedUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _teamService.UpdateProfileAsync(updatedUser);
                if (result)
                {
                    return RedirectToAction("ProfileView", new { userId = updatedUser.UserId });
                }
                ModelState.AddModelError("", "Error updating profile.");
            }

            return View("ProfileView", updatedUser); // Return the updated user for re-editing
        }
    }


    //public IActionResult Teams()
    //    {
    //        return View();
    //    }
}

