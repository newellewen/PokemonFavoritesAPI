namespace PokemonFavoritesAPI.Database.DTOs;

public class AddFavoritePokemonRequest
{
    public int UserId { get; set; }
    public int PokemonId { get; set; }
    public string? Name { get; set; }
}