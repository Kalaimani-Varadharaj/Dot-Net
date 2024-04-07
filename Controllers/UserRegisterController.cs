using FraudDetectionRepositoryPatternProject.BO;
using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetectionRepositoryPatternProject.Controllers
{
    [Controller]
    public class UserRegisterController : Controller
    {
        private UserRegisterBo _userRegisterBo;

        public UserRegisterController(UserRegisterBo userRegisterBo)
        {
            _userRegisterBo = userRegisterBo;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister newUser)
        {
            // Check if the username already exists
            if (_userRegisterBo.UsernameExists(newUser.UserName))
            {
                // Handle registration failure
                ModelState.AddModelError("", "Username already exists");
                return View(newUser);
            }

            // Username is unique, proceed with registration
            if (_userRegisterBo.RegisterUser(newUser))
            {
                // Successful registration
                return RedirectToAction("Login");
            }

            // Handle other registration failure scenarios
            ModelState.AddModelError("", "Registration failed");
            return View(newUser);
        }

        [HttpGet] // temporarily allow GET requests for testing
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            // Validate login credentials
            if (_userRegisterBo.ValidateLogin(login))
            {
                // Successful login, you can redirect to a dashboard or another page
                return RedirectToAction("FindAllIncidents", "FraudulentIncidentDetail");

            }

            // Handle login failure
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }

    }
}
