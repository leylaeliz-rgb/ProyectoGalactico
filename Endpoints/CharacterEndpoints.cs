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
                .WithDescription("Filtra por facción (Rebelde, Imperio, Neutral) y por sensibilidad a la Fuerza.")
                .ProducesList<Character>();

            group.MapGet("/{id:int}", (CharacterService service, int id) =>
                    service.GetById(id).ToHttp())
                .WithGroupName("v1")
                .WithSummary("Obtiene un personaje por id")
                .ProducesRead<Character>();

            //defensa 

            //group.MapGet("/", (CardService service, int? characterId, int? minDangerousLevel) =>
            //        Results.Ok(service.GetAll(characterId, minDangerousLevel)))
            //    .WithGroupName("v1")
            //    .WithSummary("Lista cartas con filtros opcionales")
            //    .WithDescription("Filtra por personaje (characterId) y por peligrosidad mínima.")
            //    .Produces<List<CardsCharacter>>();

            //group.MapGet("/", (CharacterService service, CardService service2, int ? characterId, int minDangerousLevel,string? faccion, bool? fuerzaSensitivo) =>
            //       service.GetAll(faccion, fuerzaSensitivo).ToHttp())
            //   .WithGroupName("v1")
            //   .WithSummary("Lista personajes con filtros opcionales")
            //   .WithDescription("Filtra por facción (Rebelde, Imperio, Neutral) y por sensibilidad a la Fuerza.")
            //   .ProducesList<Character>();

            //// defensa
            group.MapPost("/", (CharacterService service, CharacterInput input) =>
            {
                var result = service.Create(input);
                return result.Status == ResultStatus.Ok
                    ? Results.Created($"/personajes/{result.Value!.Id}", result.Value)
                    : result.ToHttp();
            })
                .WithGroupName("v2")
                .WithSummary("Crea un personaje")
                .ProducesCreate<Character>();

            group.MapPut("/{id:int}", (CharacterService service, int id, CharacterInput input) =>
                    service.Update(id, input).ToHttp())
                .WithGroupName("v2")
                .WithSummary("Modifica un personaje")
                .ProducesAction<Character>();

            group.MapDelete("/{id:int}", (CharacterService service, int id) =>
            {
                var result = service.Delete(id);
                return result.Status == ResultStatus.Ok ? Results.NoContent() : result.ToHttp();
            })
                .WithGroupName("v2")
                .WithSummary("Elimina un personaje")
                .ProducesDelete();
        }


    }
}
