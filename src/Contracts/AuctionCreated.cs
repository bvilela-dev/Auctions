﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class AuctionCreated
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; }
        public required string Seller { get; set; }
        public string? Winner { get; set; }
        public int SoldAmount { get; set; }
        public int CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }
        public required string Status { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public required string Color { get; set; }
        public required string ImageUrl { get; set; }
    }
}
