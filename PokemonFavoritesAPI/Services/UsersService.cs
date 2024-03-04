using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.DTOs;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IUsersService : IBaseService
{
    Task AddUser(AddUserRequest request);
}

public class UsersService : BaseService, IUsersService
{
    private readonly ILogger<UsersService> _logger;

    public UsersService(PokemonFavoritesContext dbContext, ILogger<UsersService> logger)
        : base(dbContext)
    {
        _logger = logger;
    }

    public async Task AddUser(AddUserRequest request)
    {
        await _dbContext.Users.AddAsync(
            new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password
            });

        await _dbContext.SaveChangesAsync();
    }
}