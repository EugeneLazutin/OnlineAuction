using System.ComponentModel.DataAnnotations;
using OnlineAuction.Entities;

namespace OnlineAuction.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            UserId = user.UserId;
            Name = user.Name;
            Sername = user.Sername;
            Email = user.Email;
            Password = user.Password;
            Phone = user.Phone;
            RoleId = user.RoleId;
        }

        public UserViewModel(){}

        public int UserId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public string Sername { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d{2}\s\d{3}-\d{2}-\d{2}",
            ErrorMessage = "Type phone like xx xxx-xx-xx")]
        public string Phone { get; set; }

        public int RoleId { get; set; }

        public void UpdateModel(User model)
        {
            model.UserId = UserId;
            model.Name = Name;
            model.Sername = Sername;
            model.Email = Email;
            model.Password = Password;
            model.Phone = Phone;
            model.RoleId = RoleId;
        }
    }
}