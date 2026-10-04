using ProyectoGalactico.Common;
using ProyectoGalactico.Data;
using ProyectoGalactico.Models;
using ProyectoGalactico.Models.Responses;

namespace ProyectoGalactico.Services
{
    public class EventService(CardService cards)
    {
        public ServiceResult<List<EventResponse>> GetAll(int? characterId, string? desde, string? hasta)
        {
            int desdeYear = 0, hastaYear = 0;
            if (desde is not null && !YearConverter.TryParse(desde, out desdeYear))
                return ServiceResult<List<EventResponse>>.Invalid("'desde' no es una fecha válida (ej. 10 BBY, 3 ABY).");
            if (hasta is not null && !YearConverter.TryParse(hasta, out hastaYear))
                return ServiceResult<List<EventResponse>>.Invalid("'hasta' no es una fecha válida (ej. 10 BBY, 3 ABY).");

            lock (DataForModels.Sync)
            {
                var query = DataForModels.Events.AsEnumerable();
                if (characterId is not null) query = query.Where(e => e.participantIds.Contains(characterId.Value));
                if (desde is not null) query = query.Where(e => e.year >= desdeYear);
                if (hasta is not null) query = query.Where(e => e.year <= hastaYear);

                // Orden cronológico: por eso se guarda el año como número
                var list = query.OrderBy(e => e.year).ThenBy(e => e.Id).Select(ToResponse).ToList();
                return ServiceResult<List<EventResponse>>.Ok(list);
            }
        }

        public ServiceResult<EventResponse> GetById(int id)
        {
            lock (DataForModels.Sync)
            {
                var ev = DataForModels.Events.FirstOrDefault(e => e.Id == id);
                return ev is null
                    ? ServiceResult<EventResponse>.NotFound($"No existe el evento {id}.")
                    : ServiceResult<EventResponse>.Ok(ToResponse(ev));
            }
        }

        public ServiceResult<List<EventResponse>> GetByCharacter(int characterId)
        {
            lock (DataForModels.Sync)
            {
                if (!DataForModels.Characters.Any(c => c.Id == characterId))
                    return ServiceResult<List<EventResponse>>.NotFound($"No existe el personaje {characterId}.");

                var list = DataForModels.Events
                    .Where(e => e.participantIds.Contains(characterId))
                    .OrderBy(e => e.year).ThenBy(e => e.Id)
                    .Select(ToResponse).ToList();

                return ServiceResult<List<EventResponse>>.Ok(list);
            }
        }

        private static string NameOf(int id) =>
            DataForModels.Characters.FirstOrDefault(c => c.Id == id)?.Name ?? $"Personaje {id}";

        private static EventResponse ToResponse(Event e) =>
            new(e.Id, e.name, YearConverter.Format(e.year), e.year, e.location, e.description,
                e.participantIds, e.deadIds, e.winner);
    }
}