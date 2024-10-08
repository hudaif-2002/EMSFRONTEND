//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using EMSFRONTEND.Models;
//using EMSFRONTEND.Services;
//using Microsoft.AspNetCore.Http;

//namespace EMSFRONTEND.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly LoginSignupService _loginSignupService;

//        public AuthController(LoginSignupService loginSignupService)
//        {
//            _loginSignupService = loginSignupService;
//        }

//        [HttpGet]
//        public IActionResult Signup()
//        {
//            // Optionally load any other data if needed
//            return View();
//        }


//        [HttpPost]
//        public async Task<IActionResult> Signup(UsersModel model)
//        {
//            if (model.Role == "Manager")
//            {
//                // Set default values for ManagerName and ManagerId when signing up as a Manager
//                model.ManagerName = "CEO"; // You can replace this with any fixed value you prefer
//                model.ManagerId = 1; // Default ManagerId (if any fixed ID is preferred)
//            }
//            else if (model.Role == "Employee")
//            {
//                // Validate the selected manager for employees
//                if (string.IsNullOrEmpty(model.ManagerName))
//                {
//                    ModelState.AddModelError("ManagerName", "Please select a manager for Employee signup.");
//                }
//                else
//                {
//                    // Fetch ManagerId from the selected ManagerName
//                    var manager = await _loginSignupService.GetManagerByNameAsync(model.ManagerName);
//                    if (manager != null)
//                    {
//                        model.ManagerId = manager.UserId; // Assign the ManagerId from the selected manager
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("ManagerName", "Selected manager not found. Please try again.");
//                    }
//                }
//            }

//            if (ModelState.IsValid)
//            {
//                var isSuccess = await _loginSignupService.SignupAsync(model);
//                if (isSuccess)
//                {
//                    return RedirectToAction("Login");
//                }
//                ModelState.AddModelError("", "Signup failed. Please try again with other details.");
//            }

//            // If signup fails, reload the managers list
//            var managers = await _loginSignupService.GetManagersAsync();
//            ViewBag.Managers = managers;

//            return View(model);
//        }





//        //[HttpPost]
//        //public async Task<IActionResult> Signup(UsersModel model)

//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        await _loginSignupService.SignupAsync(model);

//        //            return RedirectToAction("Login");


//        //    }
//        //    else
//        //    {
//        //        ModelState.AddModelError("", "Try with other details");
//        //    }

//        //    // Reload the managers list if the signup fails
//        //    var managers = await _loginSignupService.GetManagersAsync();
//        //    ViewBag.Managers = managers;

//        //    return View(model);
//        //}

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginRequestModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var isSuccess = await _loginSignupService.LoginAsync(model);

//                if (isSuccess)
//                {
//                    HttpContext.Session.SetString("Username", model.Username);
//                    HttpContext.Session.SetString("Role", model.Role);

//                    if (model.Role == "Manager")
//                    {
//                        return RedirectToAction("ManagerView", "Dashboard");
//                    }
//                    else if (model.Role == "Employee")
//                    {
//                        return RedirectToAction("EmployeeView", "Dashboard");
//                    }
//                }
//                ModelState.AddModelError("", "Invalid login attempt.");
//            }

//            return View(model);
//        }

//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }

//        // This method fetches all the managers from the service
//        [HttpGet]
//        public async Task<JsonResult> GetManagers()
//        {
//            var managers = await _loginSignupService.GetManagersAsync();
//            return Json(managers);
//        }
//    }
//}








using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EMSFRONTEND.Models;
using EMSFRONTEND.Services;
using Microsoft.AspNetCore.Http;

namespace EMSFRONTEND.Controllers
{
    public class AuthController : Controller
    {
        private readonly LoginSignupService _loginSignupService;

        public AuthController(LoginSignupService loginSignupService)
        {
            _loginSignupService = loginSignupService;
        }

        [HttpGet]
        public async Task<IActionResult> Signup()
        {
            // Load the list of managers
            var managers = await _loginSignupService.GetManagersAsync();
            ViewBag.Managers = managers;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Signup(UsersModel model)
        {
            if (model.Role == "Manager")
            {
                // Ensure ManagerName and ManagerId are set to fixed values for manager signup
                model.ManagerName = "CEO";  // You can replace "CEO" with your preferred default value
                model.ManagerId = 1;  // Set a default ManagerId (1 or any fixed ID)

                // No need to select a manager during manager signup
                ModelState.Remove("ManagerName");
            }
            else if (model.Role == "Employee")
            {
                // Handle employee signup (this part is already working for you)
                if (string.IsNullOrEmpty(model.ManagerName))
                {
                    ModelState.AddModelError("ManagerName", "Please select a manager for Employee signup.");
                }
                else
                {
                    var manager = await _loginSignupService.GetManagerByNameAsync(model.ManagerName);
                    if (manager != null)
                    {
                        model.ManagerId = manager.UserId;
                    }
                    else
                    {
                        ModelState.AddModelError("ManagerName", "Selected manager not found. Please try again.");
                    }
                }
            }

            // Check if the model is valid before proceeding with signup
            if (ModelState.IsValid)
            {
                var isSuccess = await _loginSignupService.SignupAsync(model);
                if (isSuccess)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Signup failed. Please try again.");
            }

            // If signup fails, reload the managers list
            var managers = await _loginSignupService.GetManagersAsync();
            ViewBag.Managers = managers;

            return View(model);
        }



        //[HttpPost]
        //public async Task<IActionResult> Signup(UsersModel model)
        //{
        //    if (model.Role == "Manager")
        //    {
        //        // Set default values for ManagerName and ManagerId when signing up as a Manager
        //        model.ManagerName = "CEO"; // You can replace this with any fixed value you prefer
        //        model.ManagerId = 1; // Default ManagerId (if any fixed ID is preferred)
        //    }
        //    else if (model.Role == "Employee")
        //    {
        //        // Ensure Manager is selected for Employee signup
        //        if (string.IsNullOrEmpty(model.ManagerName))
        //        {
        //            ModelState.AddModelError("ManagerName", "Please select a manager for Employee signup.");
        //        }
        //        else
        //        {
        //            // Fetch ManagerId from the selected ManagerName
        //            var manager = await _loginSignupService.GetManagerByNameAsync(model.ManagerName);
        //            if (manager != null)
        //            {
        //                model.ManagerId = manager.UserId; // Assign the ManagerId based on the selected manager
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("ManagerName", "Selected manager not found. Please try again.");
        //            }
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var isSuccess = await _loginSignupService.SignupAsync(model);
        //        if (isSuccess)
        //        {
        //            return RedirectToAction("Login");
        //        }
        //        ModelState.AddModelError("", "Signup failed. Please try again.");
        //    }

        //    // Reload the managers list for the dropdown if there was an error
        //    var managers = await _loginSignupService.GetManagersAsync();
        //    ViewBag.Managers = managers;

        //    return View(model);
        //}


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _loginSignupService.LoginAsync(model);

                if (isSuccess)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("Role", model.Role);

                    if (model.Role == "Manager")
                    {
                        return RedirectToAction("ManagerView", "Dashboard");
                    }
                    else if (model.Role == "Employee")
                    {
                        return RedirectToAction("EmployeeView", "Dashboard");
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Fetch all managers
        [HttpGet]
        public async Task<JsonResult> GetManagers()
        {
            var managers = await _loginSignupService.GetManagersAsync();
            return Json(managers);
        }
    }
}
