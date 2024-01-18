using Auctionieer.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auctionieer.Domain.Models;
[Table("Items")]
public class Item : Entity
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    //nav properties
    [ForeignKey("AuctionId")]
    [ValidateNever]
    public Auction Auction { get; set; }
    public string AuctionId { get; set; }
}
