using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTask.Domain.DbEntities.AccountEntities
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Password confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
