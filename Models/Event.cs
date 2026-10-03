namespace ProyectoGalactico.Models
{
    public record Event(int Id, string name, int year, string location, string
        description, List<int> participantIds, List<int> deadIds, string? winner);
    public record EventInput(string name, string year, string location,
                         string description, List<int> participantIds,
                         List<int> deadIds);
}
