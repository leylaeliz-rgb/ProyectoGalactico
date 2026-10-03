namespace ProyectoGalactico.Models
{
    public record Events(int Id, string name, int year, string location, string
        description, List<int> participantsIds, List<int> deadIds, string? winner);
    public record EventInput(string name, int year, string location,
                         string description, List<int> participantIds,
                         List<int> deadIds);
}
