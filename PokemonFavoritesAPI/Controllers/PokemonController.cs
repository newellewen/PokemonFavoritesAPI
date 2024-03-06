using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonFavoritesAPI.Services;

namespace PokemonFavoritesAPI.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{

    private readonly ILogger<PokemonController> _logger;
    private readonly IPokemonService _pokemonService;

    public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
    {
        _logger = logger;
        _pokemonService = pokemonService;
    }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] int limit, [FromQuery] int offset)
    {
        var result = await _pokemonService.GetPokemon(limit, offset);
        return Ok(result);
    }
    
    [HttpGet("{pokemonId}")]
    public async Task<IActionResult> GetPokemonById([FromRoute] int pokemonId)
    {
        var result = await _pokemonService.GetPokemonById(pokemonId);
        return Ok(result);
    }
    
}