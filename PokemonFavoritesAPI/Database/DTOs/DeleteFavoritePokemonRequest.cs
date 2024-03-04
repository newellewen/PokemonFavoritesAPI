namespace PokemonFavoritesAPI.Database.DTOs;

public class DeleteFavoritePokemonRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PokemonId { get; set; }
}