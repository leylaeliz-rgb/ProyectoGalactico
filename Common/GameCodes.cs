namespace ProyectoGalactico.Common
{
    public static class GameCodes
    {
        public static char? ParseFaction(string? text) => text?.Trim().ToLowerInvariant() switch
        {
            "rebelde" or "r" => 'R',
            "imperio" or "i" => 'I',
            "neutral" or "n" => 'N',
            _ => null
        };

        public static bool IsValidFaction(char c) => c is 'R' or 'I' or 'N';
        public static bool IsValidState(char c) => c is 'V' or 'M' or 'D'; //vivo, muerto, desconocido

    }
}
