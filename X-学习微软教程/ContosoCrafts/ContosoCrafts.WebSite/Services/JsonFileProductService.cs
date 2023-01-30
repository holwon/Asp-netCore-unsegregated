using System.Text.Json;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Services;
public class JsonFileProductService
{
    public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
    {
        WebHostEnvironment = webHostEnvironment;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }

    private string JsonFileName
    {
        //get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        get => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");
        //get => Path.Combine(Directory.GetCurrentDirectory());
    }

    public IEnumerable<Product> GetProducts()
    {
        using var jsonFileReader = File.OpenText(JsonFileName);
        var products = JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return products!;
    }
    public void AddRating(string productId, int rating)
    {
        var products = GetProducts();
        var query = products.First(x => x.Id == productId);

        if (query.Ratings == null)
        {
            query.Ratings = new int[] { rating };
        }
        else
        {
            var ratings = query.Ratings.ToList();
            ratings.Add(rating);
            query.Ratings = ratings.ToArray();
        }

        using var outputStream = File.OpenWrite(JsonFileName);
        JsonSerializer.Serialize<IEnumerable<Product>>(
            new Utf8JsonWriter(outputStream, new JsonWriterOptions
            {
                SkipValidation = true,
                Indented = true
            }),
            products
        );
    }
}