using EMSFRONTEND.Models;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction("ViewLeaveRequestStatus");
            }
            return View(leaveRequest);
        }

        public IActionResult Create()
        {
            return View(new LeaveRequestModel());
        }

        // New method to view leave requests
        public async Task<IActionResult> ViewLeaveRequestStatus()
        {
            var userId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get UserId from session
            var leaveRequests = await _leaveRequestService.GetLeaveRequestsByUserId(userId);

            return View(leaveRequests);
        }
        // Method to view leave requests for the manager
        public async Task<IActionResult> ApproveOrRejectLeave()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Manager")
            {
                return Unauthorized(); 
            }
            var managerId = HttpContext.Session.GetInt32("SUserId") ?? 0; // Get ManagerId from session
            var leaveRequests = await _leaveRequestService.GetLeaveRequestsForManager(managerId);

            return View(leaveRequests);
        }


    

        // Method to approve leave request
        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int requestId)
        {
            var success = await _leaveRequestService.ApproveLeaveRequest(requestId);
            if (success)
            {
                // You might want to add a success message here
            }
            return RedirectToAction("ApproveOrRejectLeave");
        }

        // Method to reject leave request
        [HttpPost]
        public async Task<IActionResult> RejectLeave(int requestId)
        {
            var success = await _leaveRequestService.RejectLeaveRequest(requestId);
            if (success)
            {
                // You might want to add a success message here
            }
            return RedirectToAction("ApproveOrRejectLeave");
        }

    }
}




