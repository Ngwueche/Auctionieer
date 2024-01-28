using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Services
{
    public class AuctionServiceHTTPClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuctionServiceHTTPClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Item>> GetItemsFromSearchDb()
        {
            var lastUpdated = await DB.Find<Item, string>()
                .Sort(x => x.Descending(x => x.UpdatedOn))
                .Project(x => x.UpdatedOn.ToString())
                .ExecuteFirstAsync();
            return await _httpClient.GetFromJsonAsync<List<Item>>(_configuration["AuctionsServiceUrl"] + "/api/auctions?date=" + lastUpdated);
        }
    }
}
