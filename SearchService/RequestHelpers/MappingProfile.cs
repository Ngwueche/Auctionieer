using AutoMapper;
using SearchService.Entities;
using ServiceContract;

namespace SearchService.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuctionCreated, Item>();
        }
    }
}
