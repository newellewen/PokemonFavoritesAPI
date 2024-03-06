using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Helpers;
using PokemonFavoritesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
});

builder.Services.AddHttpClient("PokeAPI", httpClient =>
{
    var pokeApiBaseUrl = builder.Configuration.GetValue<string>("BaseUrls:PokeAPI");
    httpClient.BaseAddress = new Uri(pokeApiBaseUrl);
});

builder.Services.AddTransient<CustomJwtHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<JwtBearerOptions, CustomJwtHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });


builder.Services.AddScoped<PokemonFavoritesContext>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();