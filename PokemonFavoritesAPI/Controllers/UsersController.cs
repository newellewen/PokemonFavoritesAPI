using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PokemonFavoritesAPI.Services;
using PokemonFavoritesAPI.Database.DTOs;

namespace PokemonFavoritesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IConfiguration _config;
    private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _usersService;

    public UsersController(ILogger<UsersController> logger, IUsersService usersService, IConfiguration config)
    {
        _logger = logger;
        _usersService = usersService;
        _config = config;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddUserRequest request)
    {
        await _usersService.AddUser(request);
        return Ok();
    }
    
    [HttpPost("Login")]
    public IActionResult Post([FromBody] LoginRequest loginRequest)
    {
        //your logic for login process
        //If login usrename and password are correct then proceed to generate token

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(120));

        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return Ok(token);
    }
    
}