using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammingTask.ViewModels
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "EmailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Technology")]
        public string Technology { get; set; }

        [Display(Name = "Experience in full years")]
        [Range(0, 100, ErrorMessage = "A number between {0} and {1}")]
        public int? Experience { get; set; }
    }
}