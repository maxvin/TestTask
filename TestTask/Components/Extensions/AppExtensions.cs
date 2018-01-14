using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.DepartmentService;
using TestTask.Domain.DbServices.UserService;

namespace TestTask.WebUI.Components.Extensions
{
    static class AppExtensions
    {
        public static void GetServicesByRoute(this IApplicationBuilder app, string route, IServiceCollection services)
        {
            app.Map(route, (appBuilder) => {

                appBuilder.Run(async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>All services</h1>");
                    sb.Append("<table>");
                    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Implementation</th></tr>");
                    foreach (var svc in services)
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                        sb.Append($"<td>{svc.Lifetime}</td>");
                        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync(sb.ToString());
                });

            });
        }

        public static void SetAdminCredentials(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetService<IUserDbService>();
            var departmentService = serviceProvider.GetService<IDepartmentService>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<User>>();

            var adminUser = userService.GetUserInfoByUserName("Admin");
            if(adminUser == null)
            {
                var adminDepartment = departmentService.GetDepartments().FirstOrDefault();
                if (adminDepartment == null)
                {
                    departmentService.AddDepartment(new Department {
                        Name = "Department name 1",
                        Address = "Department name 2" });
                }

                userService.AddUser(new User {
                    UserName = "Admin",
                    Name = "Admin",
                    Mobile = "123123123",
                    Department = adminDepartment,
                }, "P@ssw0rd");

                var adminRole = roleManager.Roles.FirstOrDefault(e => e.Id == "Admin");
                if (adminRole == null)
                {
                    roleManager.CreateAsync(new IdentityRole { Id = "Admin", Name = "Admin" });
                }

                userManager.AddToRoleAsync(userService.GetUserInfoByUserName("Admin"), "Admin");
            }
        }
    }
}
