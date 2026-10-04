using ProyectoGalactico.Common;
using ProyectoGalactico.Models;
using ProyectoGalactico.Services;

namespace ProyectoGalactico.Endpoints
{
    public static class EventEndpoints
    {
        public static void MapEventEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/eventos").WithTags("Eventos");

            group.MapGet("/", (EventService service, int? characterId, string? desde, string? hasta) =>
                    service.GetAll(characterId, desde, hasta).ToHttp())
                .WithGroupName("v1")
                .WithSummary("Lista eventos en orden cronológico")
                .WithDescription("Filtra por personaje (characterId) y por rango de fechas: desde=10 BBY&hasta=3 ABY.");

            
        }
    }
}