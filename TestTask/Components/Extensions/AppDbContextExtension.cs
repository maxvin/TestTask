﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Domain;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.CustomerService;
using TestTask.Domain.DbServices.DepartmentService;
using TestTask.Domain.DbServices.UserService;

namespace TestTask.WebUI.Components.Extensions
{
    public static class AppDbContextExtension
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void AddtestTaskServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUserDbService), typeof(UserDbService));
            services.AddTransient(typeof(IDepartmentService), typeof(DepartmentService));
            services.AddTransient(typeof(ICustomerService), typeof(CustomerService));

        }
    }
}
