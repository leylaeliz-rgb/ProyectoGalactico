using ProyectoGalactico.Endpoints;
using ProyectoGalactico.OpenApi;
using ProyectoGalactico.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGalacticOpenApi();

builder.Services.AddSingleton<CharacterService>();
builder.Services.AddSingleton<CardService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<BattleService>();
builder.Services.AddSingleton<RankingService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseGalacticSwagger();
}
app.MapCharacterEndpoints();
app.MapCardEndpoints();
app.MapEventEndpoints();
app.MapGameEndpoints();
app.Run();
