using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace PokemonFavoritesAPI.Helpers;

public class CustomJwtHandler : JwtBearerHandler
{
    private readonly HttpClient _httpClient;

    public CustomJwtHandler(HttpClient httpClient, IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _httpClient = httpClient;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var principal = GetClaims();
        return AuthenticateResult.Success(new AuthenticationTicket(principal, "CustomJwtBearer"));
    }


    private ClaimsPrincipal GetClaims()
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, "Phil"));
        claims.Add(new Claim(ClaimTypes.Email, "philipnewell@gmail.com"));

        var claimsIdentity = new ClaimsIdentity(claims,"Token");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return claimsPrincipal;
    }
}