using ProyectoGalactico.Endpoints;
using ProyectoGalactico.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");
builder.Services.AddSingleton<CharacterService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Proyecto Galactico Todos contra todos - Juego ");
        options.SwaggerEndpoint("/openapi/v2.json", "Proyecto Galactico - Zona de Administración");
        options.DocumentTitle = "Manual de Operaciones - Proyecto Galáctico";
        options.EnableFilter();// Habilita barra de búsqueda de endpoints
        options.DisplayRequestDuration();
    });
}

app.MapGet("/", () => "proyecto con swagger");
app.MapCharacterEndpoints();

app.Run();
