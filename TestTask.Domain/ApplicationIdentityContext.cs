using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain
{
    public class ApplicationIdentityContext : IdentityDbContext<User>
    {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options)
            : base(options)
        {
        }
    }
}
