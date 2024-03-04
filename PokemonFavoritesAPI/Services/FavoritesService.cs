using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.DTOs;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IFavoritesService : IBaseService
{
    Task<IEnumerable<Pokemon>> GetPokemon();
    Task AddFavoritePokemon(AddFavoritePokemonRequest request);
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

    public async Task AddFavoritePokemon(AddFavoritePokemonRequest request)
    {
        await _dbContext.FavoritePokemon.AddAsync(
            new()
            {
                PokemonId = request.PokemonId,
                UserId = request.UserId
            });

        await _dbContext.SaveChangesAsync();
    }
}