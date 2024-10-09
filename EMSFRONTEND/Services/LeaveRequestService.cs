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

        // You can keep the other methods if needed, or remove them for brevity.
    }
}
