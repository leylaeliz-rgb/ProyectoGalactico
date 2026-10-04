using ProyectoGalactico.Common;
using ProyectoGalactico.Models;
using ProyectoGalactico.Services;
using ProyectoGalactico.OpenApi;
namespace ProyectoGalactico.Endpoints
{
    public static class CharacterEndpoints
    {
        public static void MapCharacterEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/personajes").WithTags("Personajes");

            group.MapGet("/", (CharacterService service, string? faccion, bool? fuerzaSensitivo) =>
                    service.GetAll(faccion, fuerzaSensitivo).ToHttp())
                .WithGroupName("v1")
                .WithSummary("Lista personajes con filtros opcionales")
                .WithDescription("Filtra por facción (Rebelde, Imperio, Neutral) y por sensibilidad a la Fuerza.");

            group.MapGet("/{id:int}", (CharacterService service, int id) =>
                    service.GetById(id).ToHttp())
                .WithGroupName("v1")
                .WithSummary("Obtiene un personaje por id");

            group.MapPost("/", (CharacterService service, CharacterInput input) =>
            {
                var result = service.Create(input);
                return result.Status == ResultStatus.Ok
                    ? Results.Created($"/personajes/{result.Value!.Id}", result.Value)
                    : result.ToHttp();
            })
                .WithGroupName("v2")
                .WithSummary("Crea un personaje");

            group.MapPut("/{id:int}", (CharacterService service, int id, CharacterInput input) =>
                    service.Update(id, input).ToHttp())
                .WithGroupName("v2")
                .WithSummary("Modifica un personaje");

            group.MapDelete("/{id:int}", (CharacterService service, int id) =>
            {
                var result = service.Delete(id);
                return result.Status == ResultStatus.Ok ? Results.NoContent() : result.ToHttp();
            })
                .WithGroupName("v2")
                .WithSummary("Elimina un personaje");
        }

    }
}
