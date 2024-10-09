using EMSFRONTEND.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EMSFRONTEND.Services
{
    public class EmpTaskListService
    {
        private readonly HttpClient _httpClient;

        public EmpTaskListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsersModel>> GetEmployeesForManagerAsync()
        {
            var response = await _httpClient.GetAsync("api/EmpTaskListApi/AssignTask");
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<UsersModel>>(stream);
            }
            return new List<UsersModel>();
        }

        public async Task<bool> AssignTaskAsync(TaskModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskListApi/AssignTask", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TaskModel>> GetTasksForEmployeeAsync()
        {
            var response = await _httpClient.GetAsync("api/EmpTaskListApi/TaskList");
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TaskModel>>(stream);
            }
            return new List<TaskModel>();
        }

        public async Task<List<TaskModel>> GetTasksForUploadAsync()
        {
            var response = await _httpClient.GetAsync("api/EmpTaskListApi/UploadTask");
            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TaskModel>>(stream);
            }
            return new List<TaskModel>();
        }

        public async Task<bool> UploadTaskAsync(UploadModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskListApi/UploadTask", model);
            return response.IsSuccessStatusCode;
        }
    }
}
