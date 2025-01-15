using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Plaese enter a valid email address")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a valid mobile number")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$")]
        public string Mobile { get; set; }
    }
}
