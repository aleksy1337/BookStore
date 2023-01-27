using System.ComponentModel.DataAnnotations;

namespace BookStore1.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is requaierd")]
        public  string Email{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
