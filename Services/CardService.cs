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

        public ServiceResult<CardsCharacter> Create(CardsCharacterInput input)
        {
            lock (DataForModels.Sync)
            {
                var error = Validate(input);
                if (error is not null) return ServiceResult<CardsCharacter>.Invalid(error);

                var id = DataForModels.Cards.Count == 0 ? 1 : DataForModels.Cards.Max(c => c.Id) + 1;

                var created = new CardsCharacter(id, input.characterId, input.power.Trim(),
                    input.SpecialHability.Trim(), input.weapon.Trim(),
                    input.dangerousLevel, input.pictureUrl.Trim());

                DataForModels.Cards.Add(created);
                return ServiceResult<CardsCharacter>.Ok(created);
            }
        }

        public ServiceResult<CardsCharacter> Update(int id, CardsCharacterInput input)
        {
            lock (DataForModels.Sync)
            {
                var index = DataForModels.Cards.FindIndex(c => c.Id == id);
                if (index < 0) return ServiceResult<CardsCharacter>.NotFound($"No existe la carta {id}.");

                var error = Validate(input);
                if (error is not null) return ServiceResult<CardsCharacter>.Invalid(error);

                var updated = DataForModels.Cards[index] with
                {
                    characterId = input.characterId,
                    power = input.power.Trim(),
                    SpecialHability = input.SpecialHability.Trim(),
                    weapon = input.weapon.Trim(),
                    dangerousLevel = input.dangerousLevel,
                    pictureUrl = input.pictureUrl.Trim()
                };

                DataForModels.Cards[index] = updated;
                return ServiceResult<CardsCharacter>.Ok(updated);
            }
        }

        // Se llama siempre dentro del lock, porque lee la lista de personajes
        private static string? Validate(CardsCharacterInput i)
        {
            if (!DataForModels.Characters.Any(c => c.Id == i.characterId))
                return $"No existe el personaje {i.characterId}.";
            if (string.IsNullOrWhiteSpace(i.power)) return "El poder es obligatorio.";
            if (string.IsNullOrWhiteSpace(i.SpecialHability)) return "La habilidad especial es obligatoria.";
            if (string.IsNullOrWhiteSpace(i.weapon)) return "El arma es obligatoria.";
            if (i.dangerousLevel is < 1 or > 10) return "El nivel de peligrosidad debe estar entre 1 y 10.";
            if (!Uri.TryCreate(i.pictureUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                return "La imagen debe ser una URL válida (http o https).";
            return null;
        }
    }
}