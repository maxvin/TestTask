using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
