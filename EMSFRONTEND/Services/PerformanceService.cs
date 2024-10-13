using EMSFRONTEND.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace EMSFRONTEND.Services
{
    public class PerformanceService
    {
        private readonly HttpClient _httpClient;

        public PerformanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293");
        }
       
        public async Task<IEnumerable<PerformanceWithFullNameDto>> GetPerformancesByManager(int managerId)
        {
            try
            {
                // Make the HTTP GET request to the API
                var response = await _httpClient.GetAsync($"api/performance/manager/{managerId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response to the expected DTO
                    return JsonConvert.DeserializeObject<IEnumerable<PerformanceWithFullNameDto>>(jsonResponse);
                }

                // Optionally log the response status code for debugging purposes
                Console.WriteLine($"Failed to fetch performances. Status code: {response.StatusCode}");

                // Return an empty list in case of failure
                return new List<PerformanceWithFullNameDto>();
            }
            catch (Exception ex)
            {
                // Optionally log the exception for further investigation
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an empty list if an exception occurs
                return new List<PerformanceWithFullNameDto>();
            }
        }


        public async Task<PerformanceWithFullNameDto> GetPerformanceByUser(int userId)
        {
            try
            {
                // Make the HTTP GET request to the API
                var response = await _httpClient.GetAsync($"api/performance/user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response to the expected DTO
                    return JsonConvert.DeserializeObject<PerformanceWithFullNameDto>(jsonResponse);
                }

                // Log the response status code for debugging purposes
                Console.WriteLine($"Failed to fetch performance. Status code: {response.StatusCode}");

                // Return null in case of failure
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return null if an exception occurs
                return null;
            }
        }

    }
}