using System.ComponentModel.DataAnnotations;

namespace Data.Request.User
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "სახელი ცარიელია", AllowEmptyStrings = false)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "გვარი ცარიელია", AllowEmptyStrings = false)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "ტელეფონის ნომერი ცარიელია", AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
    }
}