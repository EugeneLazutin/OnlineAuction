namespace OnlineAuction.Entities
{
    public class Bet
    {
        public int BetId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public decimal Value { get; set; }
    }
}
