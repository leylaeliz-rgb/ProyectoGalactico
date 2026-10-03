using ProyectoGalactico.Common;
using ProyectoGalactico.Data;
using ProyectoGalactico.Models;

namespace ProyectoGalactico.Services
{
    public class CardService
    {
        public List<CardsCharacter> GetAll(int? characterId, int? minDangerousLevel)
        {
            lock (DataForModels.Sync)
            {
                var query = DataForModels.Cards.AsEnumerable();
                if (characterId is not null) query = query.Where(c => c.characterId == characterId);
                if (minDangerousLevel is not null) query = query.Where(c => c.dangerousLevel >= minDangerousLevel);
                return query.ToList();
            }
        }

        public ServiceResult<CardsCharacter> GetById(int id)
        {
            lock (DataForModels.Sync)
            {
                var card = DataForModels.Cards.FirstOrDefault(c => c.Id == id);
                return card is null
                    ? ServiceResult<CardsCharacter>.NotFound($"No existe la carta {id}.")
                    : ServiceResult<CardsCharacter>.Ok(card);
            }
        }

        // Si un personaje tiene varias cartas, cuenta la de mayor peligrosidad
        public CardsCharacter? GetBestCard(int characterId)
        {
            lock (DataForModels.Sync)
            {
                return DataForModels.Cards
                    .Where(c => c.characterId == characterId)
                    .OrderByDescending(c => c.dangerousLevel)
                    .FirstOrDefault();
            }
        }
    }
}