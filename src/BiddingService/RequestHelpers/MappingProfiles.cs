using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BiddingService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bid, BidDto>();
            CreateMap<Bid, BidPlaced>();
        }
    }
}
