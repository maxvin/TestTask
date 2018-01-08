using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestTask.Domain.DbEntities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("User")]
        public virtual User Manager { get; set; }

        [Required]
        public string Address { get; set; }

    }
}
