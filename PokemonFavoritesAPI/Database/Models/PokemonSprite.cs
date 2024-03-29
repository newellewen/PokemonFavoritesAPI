using System.Text.Json.Serialization;

namespace PokemonFavoritesAPI.Database.Models;

public class PokemonSprite
{
    [JsonPropertyName("back_shiny")]
    public string BackShiny { get; set; }
    [JsonPropertyName("front_shiny")]
    public string FrontShiny { get; set; }
}