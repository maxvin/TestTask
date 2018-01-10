using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestTask.Domain.DbEntities
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^([0|\\+[0-9]{1,5})?([7-9][0-9]{9})$")]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual Department Department { get; set; }

        public bool IsUserManager { get; set; }

    }
}
