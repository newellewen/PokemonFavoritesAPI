using Microsoft.AspNetCore.Mvc;
using PokemonFavoritesAPI.Services;

namespace PokemonFavoritesAPI.Controllers;

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
    public async Task<IActionResult> Get()
    {
        var result = await _favoritesService.GetPokemon();
        
        return Ok(result);
    }
}