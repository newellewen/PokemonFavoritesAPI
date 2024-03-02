using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonFavoritesAPI.Database.Models;

[Table("pokemon")]
public class Pokemon
{
    [Key]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
}