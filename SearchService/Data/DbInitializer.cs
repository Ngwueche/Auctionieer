using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            await DB.InitAsync("SearchDb", MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

            await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();

            using var scoped = app.Services.CreateScope();
            var httpClient = scoped.ServiceProvider.GetRequiredService<AuctionServiceHTTPClient>();
            var items = await httpClient.GetItemsFromSearchDb();

            Console.WriteLine(items.Count + "returned from auction service");

            if (items.Count > 0) await DB.SaveAsync(items);

            //MONGODB SEEDER
            //if (count == 0)
            //{
            //    Console.WriteLine("No Data -- Seeding data now ...");
            //    //read the file
            //    var itemData = await File.ReadAllTextAsync("Data/auction.json");//READS FILE CONTENT
            //    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //    var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

            //    await DB.SaveAsync(items);
            //}
            //if (count > 0)
            //{
            //    Console.WriteLine("Data Exist");
            //}
        }
    }
}
