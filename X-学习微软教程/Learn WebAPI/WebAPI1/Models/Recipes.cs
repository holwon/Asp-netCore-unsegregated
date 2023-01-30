using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebAPI1.Models;

public record Recipes
{
    // [JsonPropertyName]
    public string Title { get; init; }
    //[JsonProperty]
    public string Description { get; init; }
    public IEnumerable<string> Direction { get; init; }
    public DateTime Updated { get; init; }
}
