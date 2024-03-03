using PokemonFavoritesAPI.Database;

namespace PokemonFavoritesAPI.Services;

public interface IBaseService { }

public abstract class BaseService : IBaseService
{
    protected readonly PokemonFavoritesContext _dbContext;

    protected BaseService(PokemonFavoritesContext dbContext)
    {
        _dbContext = dbContext;
    }
}