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

        public async Task<IEnumerable<PerformanceModel>> GetPerformancesByManager(int managerId)
        {
            var response = await _httpClient.GetAsync($"api/performance/manager/{managerId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                /*var performances = JsonConvert.DeserializeObject<List<PerformanceModel>>(jsonResponse);
                return performances;*/
                return JsonConvert.DeserializeObject<IEnumerable<PerformanceModel>>(jsonResponse);
            }
            return new List<PerformanceModel>();
        }
        // New method to create a performance record
        public async Task<bool> CreatePerformance(PerformanceModel performance)
        {
            var jsonContent = JsonConvert.SerializeObject(performance);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/performance", contentString);
            return response.IsSuccessStatusCode;
        }
    }
}