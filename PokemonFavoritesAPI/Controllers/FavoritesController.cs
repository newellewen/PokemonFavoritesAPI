using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonFavoritesAPI.Services;
using PokemonFavoritesAPI.Database.DTOs;

namespace PokemonFavoritesAPI.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{

    private readonly ILogger<FavoritesController> _logger;
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(ILogger<FavoritesController> logger, IFavoritesService favoritesService)
    {
        _logger = logger;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int userId)
    {
        var result = await _favoritesService.GetFavoritePokemonByUserId(userId);
        
        return Ok(result);
    }

    [HttpGet("IsFavorite")]
    public async Task<IActionResult> IsPokemonUserFavorite([FromQuery] int userId, [FromQuery] int pokemonId)
    {
        var result = await _favoritesService.IsPokemonUserFavorite(userId, pokemonId);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddFavoritePokemonRequest request)
    {
        await _favoritesService.AddFavoritePokemon(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int userId, [FromQuery] int pokemonId)
    {
        await _favoritesService.DeleteFavoritePokemon(userId, pokemonId);
        return Ok();
    }
    
}