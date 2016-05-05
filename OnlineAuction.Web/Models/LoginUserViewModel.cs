using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Models
{
    public class LoginUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}