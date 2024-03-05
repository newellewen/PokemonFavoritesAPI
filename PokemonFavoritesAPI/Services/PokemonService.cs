using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IPokemonService : IBaseService
{
    Task<string> GetPokemon(int limit, int offset);
    Task<Pokemon> GetPokemonById(int pokemonId);
}

public class PokemonService : BaseService, IPokemonService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PokemonService> _logger;

    public PokemonService(PokemonFavoritesContext dbContext, IHttpClientFactory httpClientFactory, ILogger<PokemonService> logger)
        : base(dbContext)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string> GetPokemon(int limit, int offset)
    {
        var httpClient = _httpClientFactory.CreateClient("PokeAPI");
        var httpResponseMessage = await httpClient.GetAsync(
            $"pokemon?limit={limit}&offset={offset}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            using (StreamReader reader = new StreamReader(contentStream))
            {
                string text = reader.ReadToEnd();
                return text;
            }
        }

        return "Something went wrong";
    }
    
    public async Task<Pokemon> GetPokemonById(int pokemonId)
    {
        var httpClient = _httpClientFactory.CreateClient("PokeAPI");
        var httpResponseMessage = await httpClient.GetAsync(
            $"pokemon/{pokemonId}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            using (StreamReader reader = new StreamReader(contentStream))
            {
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<Pokemon>(json, 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })!;
            }
        }

        return null;
    }
}