namespace ProyectoGalactico.Models
{
    public record Character(int Id, string Name, string species, char faccion,
                            string afiliacion, char estado, bool fuerzaSensitivo, string? urlPicture);
    public record CharacterInput(string name, string species, char faccion,
                            string afiliacion, char estado, bool fuerzaSensitivo, string? urlPicture);
}
