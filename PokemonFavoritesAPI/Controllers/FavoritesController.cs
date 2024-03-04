using Microsoft.AspNetCore.Mvc;
using PokemonFavoritesAPI.Services;
using PokemonFavoritesAPI.Database.DTOs;

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
    public async Task<IActionResult> Get(GetFavoritePokemonRequest request)
    {
        var result = await _favoritesService.GetFavoritePokemonByUserId(request);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddFavoritePokemonRequest request)
    {
        await _favoritesService.AddFavoritePokemon(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFavoritePokemonRequest request)
    {
        await _favoritesService.DeleteFavoritePokemon(request);
        return Ok();
    }
    
}