using Final.Models.Entities;

namespace Final.Models.ViewModels;

// Exact same as the normal Trainer entity class

public class DeleteTrainerVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Money { get; set; }
    public ICollection<Pokemon>? Pokemons { get; set; }
}


