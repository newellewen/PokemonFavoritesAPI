using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.DTOs;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IFavoritesService : IBaseService
{
    Task<IEnumerable<FavoritePokemon>> GetFavoritePokemonByUserId(int userId);
    Task<bool> IsPokemonUserFavorite(int userId, int pokemonId);
    Task AddFavoritePokemon(AddFavoritePokemonRequest request);
    Task DeleteFavoritePokemon(int userId, int pokemonId);
}

public class FavoritesService : BaseService, IFavoritesService
{
    public FavoritesService(PokemonFavoritesContext dbContext)
        : base(dbContext)
    { }

    public async Task<IEnumerable<FavoritePokemon>> GetFavoritePokemonByUserId(int userId)
    {
        return await _dbContext.FavoritePokemon
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> IsPokemonUserFavorite(int userId, int pokemonId)
    {
        return _dbContext.FavoritePokemon
            .Any(f => f.UserId == userId && f.PokemonId == pokemonId);
    }
    
    public async Task AddFavoritePokemon(AddFavoritePokemonRequest request)
    {
        await _dbContext.FavoritePokemon.AddAsync(
            new()
            {
                PokemonId = request.PokemonId,
                UserId = request.UserId,
                Name = request.Name!,
                Types = request.Types!,
                Thumbnail = request.Thumbnail!
            });

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFavoritePokemon(int userId, int pokemonId)
    {
        var favoritesToDelete = await _dbContext.FavoritePokemon
            .Where(f => f.UserId == userId && f.PokemonId == pokemonId)
            .ToListAsync();
        
        _dbContext.FavoritePokemon.RemoveRange(favoritesToDelete);

        await _dbContext.SaveChangesAsync();
    }
}