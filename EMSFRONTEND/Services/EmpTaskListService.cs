

using EMSFRONTEND.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace EMSFRONTEND.Services
{
    // Service class for managing employee task lists
    public class EmpTaskListService
    {
        private readonly HttpClient _httpClient;

        // Constructor to initialize HttpClient with the base address of the API
        public EmpTaskListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293");
        }

        // Method to retrieve a list of employees for a given manager
        public async Task<IEnumerable<UsersModel>> GetEmployeesForManager(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/GetEmployees/{userId}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize to a list of UsersModel
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UsersModel>>(jsonResponse) ?? new List<UsersModel>();
            }
            // Return an empty list if the API call was not successful
            return new List<UsersModel>();
        }

        // Method to assign a task to an employee
        public async Task<bool> AssignTask(TaskModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskList/AssignTask", model);
            if (!response.IsSuccessStatusCode)
            {
                // Log the error response if the API call fails
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }
            return response.IsSuccessStatusCode; // Return success status
        }

        // Method to retrieve a list of tasks for a specific employee
        public async Task<IEnumerable<TaskModel>> GetTasksForEmployee(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/TaskList/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                // Log the error message and throw an exception if the API call fails
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed: {error}");
            }

            // Deserialize the response to a list of TaskModel
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonResponse);
        }

        // Method to upload a task and handle possible errors
        public async Task<bool> UploadTask(UploadModel model, HttpContext httpContext)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/EmpTaskList/UploadTask/{model.UserId}", model);

            if (!response.IsSuccessStatusCode)
            {
                // Log the error response if the API call fails
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
                return false; // Return false to trigger redirection to the error page
            }

            return true; // Return true if the upload was successful
        }

        // Method to retrieve uploads for a specific manager
        public async Task<IEnumerable<UploadWithFullDetailsDto>> GetUploadsForManager(int managerId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/manager/uploads/{managerId}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize to a list of UploadWithFullDetailsDto
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var uploads = JsonConvert.DeserializeObject<List<UploadWithFullDetailsDto>>(jsonResponse);
                return uploads;
            }

            return new List<UploadWithFullDetailsDto>(); // Return an empty list if the API call fails
        }

        // Method to retrieve tasks under a specific manager
        public async Task<IEnumerable<TaskWithFullNameDto>> GetTasksUnderManager(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/manager/tasks/{userId}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize to a list of TaskWithFullNameDto
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<TaskWithFullNameDto>>(jsonResponse);
            }

            return new List<TaskWithFullNameDto>(); // Return an empty list if the API call fails
        }

        // Method to delete a task by its ID
        public async Task DeleteTaskAsync(int taskId)
        {
            var response = await _httpClient.DeleteAsync($"api/EmpTaskList/Delete/{taskId}");
            response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful
        }
    }
}
