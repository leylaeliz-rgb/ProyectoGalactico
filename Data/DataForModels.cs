using ProyectoGalactico.Models;

namespace ProyectoGalactico.Data
{
    public class DataForModels
    {

        // faccion: 'R' Rebelde, 'I' Imperio, 'N' Neutral
        // estado:  'V' vivo, 'M' muerto, 'D' desconocido
        public static List<Character> Characters = new()
        {
            new Character(1, "Luke Skywalker", "Humano", 'R', "Alianza Rebelde", 'V', true),
            new Character(2, "Darth Vader",    "Humano", 'I', "Imperio Galáctico", 'V', true),
            new Character(3, "Leia Organa",    "Humano", 'R', "Alianza Rebelde", 'V', false),
            new Character(4, "Han Solo",       "Humano", 'N', "Contrabandista", 'V', false),
            new Character(5, "Obi-Wan Kenobi", "Humano", 'R', "Orden Jedi", 'V', true),
            new Character(6, "Emperador Palpatine", "Humano", 'I', "Imperio Galáctico", 'V', false),
        };

        public static List<CardsCharacter> Cards = new()
        {
            new CardsCharacter(1, 1, "Fuerza", "Salto de la Fuerza", "Sable de luz azul", 8, "https://ejemplo.com/luke.png"),
            new CardsCharacter(2, 2, "Estrangulamiento", "Asfixia de la Fuerza", "Sable de luz rojo", 10, "https://ejemplo.com/vader.png"),
            new CardsCharacter(3, 3, "Liderazgo", "Persuasión", "Blaster", 4, "https://ejemplo.com/leia.png"),
            new CardsCharacter(4, 4, "Puntería", "Disparo rápido", "Blaster DL-44", 5, "https://ejemplo.com/han.png"),
            new CardsCharacter(5, 5, "Fuerza", "Mente Jedi", "Sable de luz azul", 7, "https://ejemplo.com/obiwan.png"),
            new CardsCharacter(6, 6, "Rayos", "Relámpago de la Fuerza", "Sable de luz rojo", 10, "https://ejemplo.com/palpatine.png"),
        };

        public static List<Events> Events = new()
        {
            new Events(1, "Duelo en la Estrella de la Muerte", 0, "Estrella de la Muerte",
                      "Obi-Wan se enfrenta a Vader mientras el grupo escapa.",
                      new List<int> { 2, 5 }, new List<int> { 5 }, null),

            new Events(2, "Batalla de Yavin", 0, "Yavin 4",
                      "La Alianza destruye la Estrella de la Muerte.",
                      new List<int> { 1, 2, 3, 4 }, new List<int>(), null),

            new Events(3, "Batalla de Hoth", 3, "Hoth",
                      "El Imperio ataca la base rebelde.",
                      new List<int> { 1, 2, 3, 4 }, new List<int>(), null),

            new Events(4, "Batalla de Endor", 4, "Endor",
                      "Caída del Imperio y muerte de sus líderes.",
                      new List<int> { 1, 2, 3, 4, 6 }, new List<int> { 2, 6 }, null),
        };

    }
}
