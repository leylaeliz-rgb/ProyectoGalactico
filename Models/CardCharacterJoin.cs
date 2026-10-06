namespace ProyectoGalactico.Models
{
    public record CardCharacterJoin(int Id, string name, string? pictureUrl, List <CardsCharacter> cartas);
}
