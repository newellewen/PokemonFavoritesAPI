using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.DTOs;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IFavoritesService : IBaseService
{
    Task<IEnumerable<FavoritePokemon>> GetFavoritePokemonByUserId(GetFavoritePokemonRequest request);
    Task AddFavoritePokemon(AddFavoritePokemonRequest request);
    Task DeleteFavoritePokemon(DeleteFavoritePokemonRequest request);
}

public class FavoritesService : BaseService, IFavoritesService
{
    public FavoritesService(PokemonFavoritesContext dbContext)
        : base(dbContext)
    { }

    public async Task<IEnumerable<FavoritePokemon>> GetFavoritePokemonByUserId(GetFavoritePokemonRequest request)
    {
        return await _dbContext.FavoritePokemon
            .Where(p => p.UserId == request.UserId)
            .ToListAsync();
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

    public async Task DeleteFavoritePokemon(DeleteFavoritePokemonRequest request)
    {
        _dbContext.FavoritePokemon.Remove(
            new()
            {
                Id = request.Id,
                PokemonId = request.PokemonId,
                UserId = request.UserId
            });

        await _dbContext.SaveChangesAsync();
    }
}