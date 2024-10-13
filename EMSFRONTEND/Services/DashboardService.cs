using System.Net.Http.Json; // Ensure you have this for JsonContent
using System.Text;
using EMSFRONTEND.Models;
using Newtonsoft.Json;

namespace EMSFRONTEND.Services
{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293");
        }

        // Get user profile by ID
        public async Task<UsersModel> GetProfileAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/team/ViewProfile?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsersModel>();
            }
            return null; 
        }

        // Get all users except admin
        public async Task<IEnumerable<UsersModel>> GetAllUsersExceptAdminAsync()
        {
            var response = await _httpClient.GetAsync("api/Dashboard/ViewProfile");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UsersModel>>();
            }
            return null; 
        }



        // Method to fetch employees by manager ID
        public async Task<List<UsersModel>> GetEmployeesByManagerId(int managerId)
        {
            var response = await _httpClient.GetAsync($"api/Dashboard/GetEmployeesByManagerId/{managerId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UsersModel>>();
            }

            return new List<UsersModel>(); // Return empty list on failure
        }



        // Method to delete user by ID
        public async Task<HttpResponseMessage> DeleteUserAsync(int userId)
        {
            return await _httpClient.DeleteAsync($"api/Dashboard/DeleteUser/{userId}");
        }


    }
}