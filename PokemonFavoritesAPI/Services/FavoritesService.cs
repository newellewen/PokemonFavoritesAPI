using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IFavoritesService : IBaseService
{
    Task<IEnumerable<Pokemon>> GetPokemon();
}

public class FavoritesService : BaseService, IFavoritesService
{
    public FavoritesService(PokemonFavoritesContext dbContext)
        : base(dbContext)
    { }

    public async Task<IEnumerable<Pokemon>> GetPokemon()
    {
        return await _dbContext.Pokemon.ToListAsync();
    }
}