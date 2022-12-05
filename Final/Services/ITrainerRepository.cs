using Final.Models.Entities;

namespace Final.Services;

// Simply to keep track of the methods we need to implement in the repo.

public interface ITrainerRepository
{
    public Task<Trainer> ReadTrainerAsync(int id);
    public Task<ICollection<Trainer>> ReadAllAsync();
    public Task<Trainer> CreateTrainerAsync(Trainer trainer);
    public Task UpdateTrainerAsync(int trainerId, Trainer trainer);
    public Task DeleteTrainerAsync(int trainerId);



    public Task<Pokemon> ReadPokemonAsync(int id);
    public Task<Pokemon> CreatePokemonAsync(int trainerId, Pokemon pokemon);
    public Task DeletePokemonAsync(int trainerId, int pokemonId);
    public Task UpdatePokemonAsync(int trainerId, Pokemon pokemon);

}

