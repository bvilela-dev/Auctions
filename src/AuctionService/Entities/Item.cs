using AuctionService.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public required string Color { get; set; }
        public required string ImageUrl { get; set; }

        // navigation properties
        public Auction? Auction { get; set; } = null!;
        public Guid AuctionId { get; set; }
    }
}