using Auctionieer.Core.DTOs;
using Auctionieer.Data;
using Auctionieer.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auctionieer.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionDbContext _context;
        private readonly IMapper _mapper;

        public AuctionsController( AuctionDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet( "auctions" )]
        public async Task<ActionResult<List<AuctionDto>>> GetAll()
        {
            List<Auction> auctions = await _context.Auctions.Include( x => x.Item ).OrderBy( x => x.Item.Make ).ToListAsync();

            if (auctions != null) return _mapper.Map<List<AuctionDto>>( auctions );
            return NotFound();
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<AuctionDto>> GetById( Guid id )
        {
            Auction auctions = await _context.Auctions.Include( x => x.Item ).FirstOrDefaultAsync( x => x.Id == id );

            if (auctions != null) return _mapper.Map<AuctionDto>( auctions );
            return NotFound();
        }

        [HttpPost( "create" )]
        public async Task<ActionResult<AuctionDto>> CreateAuction( CreateAuctionDto createAuction )
        {
            Auction auction = await _context.Auctions.FindAsync( createAuction.Id );
            if (auction != null) return BadRequest( "Auction already exist" );
            if (ModelState.IsValid)
            {
                var mapAuction = _mapper.Map<Auction>( createAuction );
                _context.Auctions.Add( mapAuction );
            }
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest( "Could not save changes to DB" );

            return CreatedAtAction( nameof( GetById ), new { auction.Id }, _mapper.Map<AuctionDto>( auction ) );
        }
        [HttpPost( "{id}" )]
        public async Task<ActionResult> UpdateAuction( Guid id, UpdateAuctionDto updateAuctionDto )
        {
            var auction = await _context.Auctions.Include( v => v.Id == id ).FirstOrDefaultAsync();
            if (auction != null)
            {
                auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
                auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
                auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
                auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
                auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return BadRequest( "Error saving Update" );
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete( " {id}" )]
        public async Task<ActionResult> DeleteAuction( Guid id )
        {
            Auction auction = await _context.Auctions.FirstOrDefaultAsync( x => x.Id == id );
            if (auction == null) return BadRequest( NotFound() );

            //TODO: check seller == username
            _context.Auctions.Remove( auction );
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest( "Error saving Update" );
            return Ok();
        }
    }
}
