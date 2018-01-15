using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbEntities.AccountEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Domain.DbServices.UserService;
using TestTask.Domain.DbServices.DepartmentService;
using TestTask.WebUI.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace TestTask.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        private readonly IUserDbService _userDbService;

        private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;

        private readonly IDepartmentService _departmentService;


        public AccountController(IUserDbService userDbService,
            IDepartmentService departmentService,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
            UserManager<User> userManager)
        {
            _userDbService = userDbService;
            _departmentService = departmentService;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated && User.HasClaim(ClaimTypes.Role, "Admin")) return RedirectToAction("Index", "Admin");
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "User");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(model.Username);
                    var isInRole = await _userManager.IsInRoleAsync(user, "Admin");

                    _logger.LogInformation("User logged in.");
                    if(isInRole) return RedirectToAction("Index", "Admin");
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }


    }
}
