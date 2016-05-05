using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using Core;
using OnlineAuction.Entities;

namespace OnlineAuction.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Sername { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        [MinLength(4)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{2}\s\d{3}-\d{2}-\d{2}", 
            ErrorMessage = "Type phone like xx xxx-xx-xx")]
        public string Phone { get; set; }

        public void UpdateModel(User model)
        {
            model.Name = Name;
            model.Sername = Sername;
            model.Email = Email;
            model.Password = Crypto.HashPassword(Password);
            model.Phone = Phone;
            model.RoleId = (int)Roles.User;
        }
    }
}