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
    }
}
