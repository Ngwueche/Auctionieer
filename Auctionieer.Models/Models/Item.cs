using Auctionieer.Models.Entities;

namespace Auctionieer.Domain.Models;

public class Item : Entity
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    //nav properties
    public Auction Auction { get; set; }
    public Guid AuctionId { get; set; }
}
