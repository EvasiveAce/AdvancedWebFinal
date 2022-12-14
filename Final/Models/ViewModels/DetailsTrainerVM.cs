using Final.Models.Entities;

namespace Final.Models.ViewModels;

// Exact same as Trainer entity class

public class DetailsTrainerVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Money { get; set; }
    public int NumberOfPokemon { get; set; }

    public ICollection<Pokemon>? Pokemons { get; set; }
}