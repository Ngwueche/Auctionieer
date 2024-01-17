using Auctionieer.Domain.Models;

namespace Auctionieer.Models.Entities
{
    public class Auction : Entity, IAuditable
    {
        public int ReservedPrice { get; set; }
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime AuctionEnd { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; }
        public Item Item { get; set; }
    }
}
