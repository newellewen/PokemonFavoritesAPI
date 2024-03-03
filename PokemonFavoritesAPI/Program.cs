using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PokemonFavoritesAPI.Database;
using PokemonFavoritesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("PokeAPI", httpClient =>
{
    var pokeApiBaseUrl = builder.Configuration.GetValue<string>("BaseUrls:PokeAPI");
    httpClient.BaseAddress = new Uri(pokeApiBaseUrl);
});

builder.Services.AddScoped<PokemonFavoritesContext>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IPokemonService, PokemonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();