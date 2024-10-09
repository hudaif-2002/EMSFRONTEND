using EMSFRONTEND.Models;
using Microsoft.AspNetCore.Mvc;
using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using System.Threading.Tasks;

namespace EMSFRONTEND.Controllers
{

    public class LeaveRequestMvcController : Controller
    {
        private readonly LeaveRequestService _leaveRequestService;

        public LeaveRequestMvcController(LeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequestModel leaveRequest)
        {
            leaveRequest.UserId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get UserId from session

            if (ModelState.IsValid)
            {
                await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                return RedirectToAction("Index");
            }
            return View(leaveRequest);
        }

        public IActionResult Create()
        {
            return View(new LeaveRequestModel());
        }

        // Other actions...
    }
}




