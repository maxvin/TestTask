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

namespace TestTask.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        private readonly IUserDbService _userDbService;

        private readonly SignInManager<User> _signInManager;

        private readonly IDepartmentService _departmentService;


        public AccountController(IUserDbService userDbService,
            IDepartmentService departmentService,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userDbService = userDbService;
            _departmentService = departmentService;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.DepartmentsList = new SelectList(_departmentService.GetDepartments().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel) {

            var departments = _departmentService.GetDepartments();

            if (ModelState.IsValid)
            {
                var registerDepartment = departments.First(e=>e.Id == registerModel.DepartmentId);
                User user = new User { UserName = registerModel.UserName,
                    Department = registerDepartment,
                    Email = registerModel.Email,
                    Mobile = registerModel.Mobile,
                    Name = registerModel.UserName
                };
                var result = await _userDbService.AddUser(user, registerModel.Password);

                if (result.Succeeded) return RedirectToAction("Site", "Index");
            }

            ViewBag.DepartmentsList = new SelectList(departments.ToList(), "Id", "Name"); ;
            return View(registerModel);

        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index","Admin");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Admin");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            return View();
        }

    }
}
