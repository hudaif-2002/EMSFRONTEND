//using EMSFRONTEND.Models;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace EMSFRONTEND.Services
//{
//    public class LoginSignupService
//    {
//        private readonly HttpClient _httpClient;

//        public LoginSignupService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            _httpClient.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
//        }

//        public async Task<bool> SignupAsync(UsersModel model)
//        {
//            var json = JsonConvert.SerializeObject(model);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            var result = await _httpClient.PostAsync("/api/auth/signup", content);

//            return result.IsSuccessStatusCode; // Return true if signup is successful
//        }

//        public async Task<bool> LoginAsync(LoginRequestModel model)
//        {
//            var json = JsonConvert.SerializeObject(model);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");
//            var result = await _httpClient.PostAsync("/api/auth/login", content);

//            return result.IsSuccessStatusCode; // Return true if login is successful
//        }

//        // Method to get the list of managers
//        public async Task<List<UsersModel>> GetManagersAsync()
//        {
//            var response = await _httpClient.GetAsync("/api/auth/getmanagers"); // Adjust the endpoint as per your API
//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<List<UsersModel>>(json);
//            }

//            return new List<UsersModel>(); // Return an empty list if the request failed
//        }

//        // New method to get a specific manager by name
//        public async Task<UsersModel> GetManagerByNameAsync(string managerName)
//        {
//            var response = await _httpClient.GetAsync($"/api/auth/getmanagerbyname?name={managerName}");
//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<UsersModel>(json);
//            }

//            return null; // Return null if the manager is not found
//        }
//    }
//}



using EMSFRONTEND.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMSFRONTEND.Services
{
    public class LoginSignupService
    {
        private readonly HttpClient _httpClient;

        public LoginSignupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
        }

        public async Task<bool> SignupAsync(UsersModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/api/auth/signup", content);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(LoginRequestModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/api/auth/login", content);

            return result.IsSuccessStatusCode;
        }




        // Method to fetch the manager by their name
        public async Task<UsersModel> GetManagerByNameAsync(string managerName)
        {
            var response = await _httpClient.GetAsync($"/api/auth/getmanagerbyname/{managerName}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UsersModel>(json);
            }

            return null;
        }


        //public async Task<UsersModel> GetManagerByNameAsync(string managerName)
        //{
        //    var response = await _httpClient.GetAsync($"/api/auth/getmanagerbyname/{managerName}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<UsersModel>(json);
        //    }

        //    return null;
        //}

        // Method to fetch the list of all managers
        public async Task<List<UsersModel>> GetManagersAsync()
        {
            var response = await _httpClient.GetAsync("/api/auth/getmanagers");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UsersModel>>(json);
            }

            return new List<UsersModel>();
        }
    }
}
