using System.ComponentModel.DataAnnotations;

namespace Final.Models.Entities;

// Simple Pokemon entity, based on the GameFreak series "Pokémon".
// If required, string empty, if not, string?

public class Pokemon
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [StringLength(3)]
    public int Level { get; set; }
    [Required]
    public string Gender { get; set; } = string.Empty;
    [StringLength(4)]
    public int DexNumber { get; set; }

    public string Nature { get; set; } = string.Empty;
    public string? Ability { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? SecondaryType { get; set; }
    public string? Item { get; set; }

    //Stats
    public int HP { get; set; }
    public int ATTACK { get; set; }
    public int DEFENCE { get; set; }
    public int SPATK { get; set; }
    public int SPDEF { get; set; }
    public int SPEED { get; set; }

    [Required]
    public bool IsShiny { get; set; }

    public Trainer? Trainer { get; set; }
}

