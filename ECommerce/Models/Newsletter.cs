using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        [Required]
        public string NewsLetter { get; set; }
        [Display(Name = "Date")]
        public string NewsLetterDate { get; set; }
    }
}
