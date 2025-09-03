using AuctionService.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService
{
    [Table("Items")]
    public class Item
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        // nav properties

        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}