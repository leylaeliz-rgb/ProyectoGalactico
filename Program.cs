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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseGalacticSwagger();
}

app.UseCord();
app.MapCharacterEndpoints();
app.MapCardEndpoints();
app.MapEventEndpoints();
app.MapGameEndpoints();
app.Run();
