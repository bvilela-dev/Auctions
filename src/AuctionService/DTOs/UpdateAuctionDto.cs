﻿namespace AuctionService.DTOs
{
    public class UpdateAuctionDto
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
    }
}
