using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    
    [HttpPost("Register")]
    public async Task<IActionResult> Post([FromBody] AddUserRequest request)
    {
        try
        {
            await _usersService.AddUser(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var user = await _usersService.GetUser(loginRequest.Username, loginRequest.Password);
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120));

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    
}