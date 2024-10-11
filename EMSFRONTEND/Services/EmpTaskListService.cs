/*using EMSFRONTEND.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
namespace EMSFRONTEND.Services
{
    public class EmpTaskListService
    {
        private readonly HttpClient _httpClient;

        public EmpTaskListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<UsersModel>> GetEmployeesForManager(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/GetEmployees/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UsersModel>>(jsonResponse) ?? new List<UsersModel>();
            }
            return new List<UsersModel>();
        }




        *//*        public async Task<IEnumerable<UsersModel>> GetEmployeesForManager(int userId)
                {
                    *//*var response = await _httpClient.GetAsync("api/EmpTaskList/GetEmployees/{userId}");*//*
                    var response = await _httpClient.GetAsync($"api/EmpTaskList/GetEmployees/{userId}");


                    if (response.IsSuccessStatusCode)
                    {
                        *//*using var stream = await response.Content.ReadAsStreamAsync();
                        return await JsonSerializer.DeserializeAsync<List<UsersModel>>(stream);*//*


                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<UsersModel>>(jsonResponse);
                    }
                    return new List<UsersModel>();
                }*//*

        public async Task<bool> AssignTask(TaskModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskList/AssignTask", model);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}"); // Log the error response
            }
            return response.IsSuccessStatusCode;
        }

        *//* public async Task<IEnumerable<TaskModel>> GetTasksForEmployee(int userId)
         {
             var response = await _httpClient.GetAsync($"api/EmpTaskListApi/TaskList/{userId}");
             if (response.IsSuccessStatusCode)
             {
                 var jsonResponse = await response.Content.ReadAsStringAsync();
                 return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonResponse);
             }
             return new List<TaskModel>();
         }*//*


        public async Task<IEnumerable<TaskModel>> GetTasksForEmployee(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/TaskList/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                // Log the error message
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed: {error}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonResponse);
        }


        *//*public async Task<IEnumerable<TaskModel>> GetTasksForUpload()
        {
            var response = await _httpClient.GetAsync("api/EmpTaskList/UploadTask");
            if (response.IsSuccessStatusCode)
            {
               *//* using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<TaskModel>>(stream);*//*

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonResponse);
            }
            return new List<TaskModel>();
        }*//*

        public async Task<bool> UploadTask(UploadModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskList/UploadTask", model);
            return response.IsSuccessStatusCode;
        }
    }
}
*/








using EMSFRONTEND.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace EMSFRONTEND.Services
{
    public class EmpTaskListService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmpTaskListService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UsersModel>> GetEmployeesForManager(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/GetEmployees/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UsersModel>>(jsonResponse) ?? new List<UsersModel>();
            }
            return new List<UsersModel>();
        }

        public async Task<bool> AssignTask(TaskModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/EmpTaskList/AssignTask", model);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}"); // Log the error response
            }
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksForEmployee(int userId)
        {
            var response = await _httpClient.GetAsync($"api/EmpTaskList/TaskList/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                // Log the error message
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API call failed: {error}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(jsonResponse);
        }

        public async Task<bool> UploadTask(UploadModel model)
        {
            // Retrieve UserId from session
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("SUserId");

            if (userId == null)
            {
                throw new Exception("User ID is required.");
            }

            model.UserId = userId.Value; // Set the UserId from session

            // Send the request to the backend API
            var response = await _httpClient.PostAsJsonAsync($"api/EmpTaskList/UploadTask/{model.UserId}", model);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}"); // Log the error response
            }
            return response.IsSuccessStatusCode;
        }
    }
}
