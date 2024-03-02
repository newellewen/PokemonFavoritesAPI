using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Database;

public class PokemonFavoritesContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public PokemonFavoritesContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<Pokemon> Pokemon { get; set; }
}