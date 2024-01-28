using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<Item>().Sort(x => x.Ascending(a => a.Make)); //PageSearch enables Pagination
            //search function
            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }

            query = searchParams.OrderBy switch
            {
                "make" => query.Sort(x => x.Ascending(a => a.Make)),
                "model" => query.Sort(x => x.Ascending(a => a.Model)),
                "color" => query.Sort(x => x.Ascending(a => a.Color)),
                "new" => query.Sort(x => x.Ascending(a => a.CreatedOn)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
            };
            query = searchParams.FilterBy switch
            {
                "finished" => query.Match(a => a.AuctionEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
                "color" => query.Sort(x => x.Ascending(a => a.Color)),
                "new" => query.Sort(x => x.Ascending(a => a.CreatedOn)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd)),
            };

            if (!string.IsNullOrEmpty(searchParams.Winner))
            {
                query.Match(x => x.Winner == searchParams.Winner);
            }
            if (!string.IsNullOrEmpty(searchParams.Seller))
            {
                query.Match(x => x.Seller == searchParams.Seller);
            }

            //pagination with mongodb
            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);

            var result = await query.ExecuteAsync();
            return Ok(new
            {
                results = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}
