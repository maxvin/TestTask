using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTask.Domain.DbEntities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public bool IsMunicipality { get; set; }

        public int? NumberOfSchools { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Department> Departments { get; set; }


        public Customer()
        {
        }
    }
}
