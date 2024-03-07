using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.DTOs;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IUsersService : IBaseService
{
    Task AddUser(AddUserRequest request);
    Task<User> GetUser(string username, string password);
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
                Password = EncodePasswordToBase64(request.Password)
            });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUser(string username, string password)
    {
        var encodedPassword = EncodePasswordToBase64(password);
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (encodedPassword == user.Username)
        {
            throw new Exception("Incorrect password");
        }

        return user;
    }
    
    private string EncodePasswordToBase64(string password)
    {
        try
        {
            var encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            var encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    private string DecodeFrom64(string encodedData)
    {
        var encoder = new System.Text.UTF8Encoding();
        var utf8Decode = encoder.GetDecoder();
        var todecode_byte = Convert.FromBase64String(encodedData);
        var charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        var decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        var result = new String(decoded_char);
        return result;
    }
}