using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;

namespace TestTask.WebUI.Controllers
{
    public class UserController : Controller
    {

        public async Task<IActionResult> Index() {
            var userManager = HttpContext.RequestServices.GetService(typeof(UserManager<User>)) as UserManager<User>;
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (userManager != null && user != null)
            {
                var isUserAdmin = await userManager.IsInRoleAsync(user, "admin");

                if (isUserAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }

            }

            return View();
        }
    }
}
