using Microsoft.AspNetCore.Mvc;
using PokemonFavoritesAPI.Services;
using PokemonFavoritesAPI.Database.DTOs;

namespace PokemonFavoritesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _usersService;

    public UsersController(ILogger<UsersController> logger, IUsersService usersService)
    {
        _logger = logger;
        _usersService = usersService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddUserRequest request)
    {
        await _usersService.AddUser(request);
        return Ok();
    }
}