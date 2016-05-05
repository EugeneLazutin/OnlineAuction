using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuction.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Sername { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
