using EMSFRONTEND.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EMSFRONTEND.Services
{
    public class TeamService
    {
        private readonly HttpClient _httpClient;

        public TeamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293"); // Base URL for the backend API
        }

        // Method to fetch employees under a specific manager
        public async Task<List<UsersModel>> GetEmployeesUnderManager(int managerId)
        {
            var response = await _httpClient.GetAsync($"/api/team/view/{managerId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UsersModel>>(jsonResponse);
            }

            return new List<UsersModel>();
        }

        public async Task<UsersModel> GetProfileAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/team/ViewProfile?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsersModel>();
            }

            return null; // Handle error appropriately
        }

        // Update user profile
        public async Task<bool> UpdateProfileAsync(UsersModel updatedUser)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/team/EditProfile?userId={updatedUser.UserId}", updatedUser);
            return response.IsSuccessStatusCode;
        }
    }
}

