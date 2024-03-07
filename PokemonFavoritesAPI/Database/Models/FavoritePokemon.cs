using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonFavoritesAPI.Database.Models;

[Table("favorite_pokemon")]
public class FavoritePokemon
{
    [Key]
    public int Id { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("pokemon_id")]
    public int PokemonId { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    [Column("types")]
    public string Types { get; set; }
    [Column("thumbnail")]
    public string Thumbnail { get; set; }
}