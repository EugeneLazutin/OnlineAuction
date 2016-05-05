using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Entities
{
    public class Lot
    {
        public int LotId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Time { get; set; }
        public decimal Price { get; set; }
    }
}