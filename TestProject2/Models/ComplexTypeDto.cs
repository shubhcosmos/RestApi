using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProject2.Models
{
    public class ComplexTypeDto
    {
        [Required]
        [MaxLength(12)]
        public string String1 { get; set; }
        public string String2 { get; set; }
        [Range(1,100)]
      public int Int1 { get; set; }
        public int Int2 { get; set; }
        public DateTime? Date1 { get; set; }
    }
}