using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Database.Models;

namespace PokemonFavoritesAPI.Services;

public interface IPokemonService : IBaseService
{
    Task<string> GetPokemon();
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

    public async Task<string> GetPokemon()
    {
        var httpClient = _httpClientFactory.CreateClient("PokeAPI");
        var httpResponseMessage = await httpClient.GetAsync(
            "v2/pokemon");

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
}