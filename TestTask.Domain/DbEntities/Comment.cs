using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTask.Domain.DbEntities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
