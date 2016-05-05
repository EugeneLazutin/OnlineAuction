using System.ComponentModel.DataAnnotations;
using OnlineAuction.Entities;

namespace OnlineAuction.Models
{
    public class LotViewModel
    {
        public int LotId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Image { get; set; }

        [Required]
        [Range(1,60)]
        public int Time { get; set; }

        [Required]
        [Range(1, 1000000)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Price { get; set; }

        public LotViewModel()
        { }

        public LotViewModel(Lot lot)
        {
            LotId = lot.LotId;
            Name = lot.Name;
            Description = lot.Description;
            Image = lot.Image;
            Time = lot.Time;
            Price = lot.Price;
        }

        public LotViewModel(int time, decimal price)
        {
            Time = time;
            Price = price;
        }

        public void SetModel(Lot model)
        {
            model.LotId = LotId;
            model.Name = Name;
            model.Description = Description;
            model.Image = Image;
            model.Time = Time;
            model.Price = Price;
        }
    }
}