using EMSFRONTEND.Models;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace EMSFRONTEND.Services
{
    public class LeaveRequestService
    {
        private readonly HttpClient _httpClient;

        public LeaveRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293");
        }

        // Create Leave Request
        public async Task<bool> CreateLeaveRequest(LeaveRequestModel leaveRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(leaveRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/leaverequest", content);
            return response.IsSuccessStatusCode;
        }
        // Get Leave Requests by User ID
        public async Task<IEnumerable<LeaveRequestModel>> GetLeaveRequestsByUserId(int userId)
        {
            var response = await _httpClient.GetAsync($"api/leaverequest/employee/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<LeaveRequestModel>>(jsonResponse);
            }
            return new List<LeaveRequestModel>();
        }
        // Get Leave Requests for Manager


        public async Task<IEnumerable<LeaveRequestModel>> GetLeaveRequestsForManager(int userId)
        {
            var response = await _httpClient.GetAsync($"api/leaverequest/manager/requests/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<LeaveRequestModel>>(jsonResponse);
            }
            return new List<LeaveRequestModel>();
        }


        // Approve Leave Request
        public async Task<bool> ApproveLeaveRequest(int requestId)
        {
            var response = await _httpClient.PutAsync($"api/leaverequest/{requestId}/approve", null);
            return response.IsSuccessStatusCode;
        }

        // Reject Leave Request
        public async Task<bool> RejectLeaveRequest(int requestId)
        {
            var response = await _httpClient.PutAsync($"api/leaverequest/{requestId}/reject", null);
            return response.IsSuccessStatusCode;
        }

    }
}
