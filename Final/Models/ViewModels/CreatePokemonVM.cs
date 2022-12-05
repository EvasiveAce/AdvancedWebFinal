using Final.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Final.Models.ViewModels;

// Create Pokemon View Model, basically goes into
// the specifics on how I want the create form to work.

public class CreatePokemonVM
{
    [StringLength(16)]
    public string Name { get; set; } = string.Empty;
    [Range(1, 100, ErrorMessage = "Cannot exceed 100")]
    public int Level { get; set; }
    public string Gender { get; set; } = string.Empty;
    [Range(1, 1008, ErrorMessage = "Enter a real Dex Number")]
    public int DexNumber { get; set; }

    public string Nature { get; set; } = string.Empty;
    public string? Ability { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? SecondaryType { get; set; }
    public string? Item { get; set; }

    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int HP { get; set; }
    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int ATTACK { get; set; }
    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int DEFENCE { get; set; }
    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int SPATK { get; set; }
    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int SPDEF { get; set; }
    [Range(1, 255, ErrorMessage = "Cannot exceed 255")]
    public int SPEED { get; set; }

    public bool IsShiny { get; set; }

    // Allows for the PokemonController to create.

    public Pokemon GetPokemonInstance()
    {
        return new Pokemon
        {
            Id = 0,
            Name = this.Name,
            Level = this.Level,
            Gender = this.Gender,
            DexNumber = this.DexNumber,
            Nature = this.Nature,
            Ability = this.Ability,
            Type = this.Type,
            SecondaryType = this.SecondaryType,
            Item = this.Item,
            HP = this.HP,
            ATTACK = this.ATTACK,
            DEFENCE = this.DEFENCE,
            SPATK = this.SPATK,
            SPDEF = this.SPDEF,
            SPEED = this.SPEED,
            IsShiny = this.IsShiny
        };
    }
}

