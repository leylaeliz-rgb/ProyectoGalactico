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
    }
}