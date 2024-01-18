using Auctionieer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auctionieer.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Auction> Auctions { get; set; }
        //public DbSet<Item> Items { get; set; }
        //you may not add other models. this because EFCore will create them provided they are linked to the Auction model. However, you will have to specify the table name on the model class.
    }
}
