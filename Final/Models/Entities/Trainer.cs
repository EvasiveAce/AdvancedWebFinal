using System.ComponentModel.DataAnnotations;

namespace Final.Models.Entities;

// Basic trainer calss, Name is required,
// Collection is for the Pokemon class.

public class Trainer
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public int Money { get; set; }

    public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
}

