using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuction.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        [MaxLength(15)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
