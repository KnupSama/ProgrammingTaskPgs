using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProgrammingTask.Entity;

namespace ProgrammingTask.ViewModels
{
    public class UserDetailsViewModel
    {
        [Display(Name = "Technology")]
        public string Technology { get; set; }

        [Required]
        [Display(Name = "Experience in full years")]
        [Range(0, 100, ErrorMessage = "Please tell us what experience you have")]
        public int Experience { get; set; }

        public IEnumerable<Technology> Technologies { get; set; }

    }
}