using Final.Models.Entities;

namespace Final.Models.ViewModels;

// Similar to the Trainer entity class, but includes
// get instance for the editing of a trainer

public class EditTrainerVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Money { get; set; }

    public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();

    // Allows for editing by TrainerController.

    public Trainer GetTrainerInstance()
    {
        return new Trainer
        {
            Id = this.Id,
            Name = this.Name,
            Money = this.Money,
            Pokemons = this.Pokemons
        };
    }
}
