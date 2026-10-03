using ProyectoGalactico.Common;
using ProyectoGalactico.Models;
using ProyectoGalactico.Services;

namespace ProyectoGalactico.Endpoints
{
    public static class CardEndpoints
    {
        public static void MapCardEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/cartas").WithTags("Cartas");

            group.MapGet("/", (CardService service, int? characterId, int? minDangerousLevel) =>
                    Results.Ok(service.GetAll(characterId, minDangerousLevel)))
                .WithGroupName("v1")
                .WithSummary("Lista cartas con filtros opcionales")
                .WithDescription("Filtra por personaje (characterId) y por peligrosidad mínima.");

            group.MapGet("/{id:int}", (CardService service, int id) =>
                    service.GetById(id).ToHttp())
                .WithGroupName("v1")
                .WithSummary("Obtiene una carta por id");

           
        }
    }
}