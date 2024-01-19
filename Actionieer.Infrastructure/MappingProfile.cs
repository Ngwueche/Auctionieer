using Auctionieer.Core.DTOs;
using Auctionieer.Domain.Models;
using Auctionieer.Models.Entities;
using AutoMapper;

namespace Auctionieer.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionDto>();
            CreateMap<CreateAuctionDto, Auction>()
                .ForMember(d => d.Item, o => o.MapFrom(s => s));
        }
    }
}
