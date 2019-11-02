using System.ComponentModel.DataAnnotations;

namespace API.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "სახელი ცარიელია", AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "პაროლი ცარიელია", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}