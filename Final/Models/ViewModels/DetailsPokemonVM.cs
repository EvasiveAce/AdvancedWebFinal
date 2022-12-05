using Final.Models.Entities;

namespace Final.Models.ViewModels;

// Exact same as the DeleteVM and Pokemon entity class

public class DetailsPokemonVM
{
    public Trainer? Trainer { get; set; }


    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int DexNumber { get; set; }

    public string Nature { get; set; } = string.Empty;
    public string? Ability { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? SecondaryType { get; set; }
    public string? Item { get; set; }

    public int HP { get; set; }
    public int ATTACK { get; set; }
    public int DEFENCE { get; set; }
    public int SPATK { get; set; }
    public int SPDEF { get; set; }
    public int SPEED { get; set; }

    public bool IsShiny { get; set; }
}

