using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class AuctionUpdated
    {
        public required string Id { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public required string Color { get; set; }
    }
}
