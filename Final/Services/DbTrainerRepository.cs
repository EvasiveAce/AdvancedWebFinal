using Final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Services;

public class DbTrainerRepository : ITrainerRepository
{

    // Normal injection of the _db Context

    private readonly ApplicationDbContext _db;
    public DbTrainerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    // ReadTrainerAsync allows for the finding of a specific Trainer
    // using FindAsync and the parameter, id.

    public async Task<Trainer> ReadTrainerAsync(int id)
    {
        var trainer = await _db.Trainers.FindAsync(id);
        if (trainer != null)
        {
            _db.Entry(trainer).Collection(t => t.Pokemons).Load();
        }
        return trainer;
    }

    // Same as ReadTrainer except does all, and is instead a Collection
    // instead of a single trainer.

    public async Task<ICollection<Trainer>> ReadAllAsync()
    {
        return await _db.Trainers.Include(t => t.Pokemons).ToListAsync();
    }

    // ReadPokemonAsync allows for the finding of a specific Pokemon
    // using FindAsync and the parameter, id.

    public async Task<Pokemon> ReadPokemonAsync(int id)
    {
        var pokemon = await _db.Pokemons.FindAsync(id);
        if (pokemon != null)
        {
            _db.Entry(pokemon);
        }
        return pokemon;
    }

    // CreateTrainerAsync allows for the adding of a trainer,
    // requires a trainer object to be added.

    public async Task<Trainer> CreateTrainerAsync(Trainer trainer)
    {
        await _db.Trainers.AddAsync(trainer);
        await _db.SaveChangesAsync();
        return trainer;
    }

    // CreatePokemonAsync allows for the adding of a pokemon,
    // requires both a trainerId and a pokemon object, since you
    // need a trainer for a pokmemon to exist in this database.

    public async Task<Pokemon> CreatePokemonAsync(int trainerId, Pokemon pokemon)
    {
        var trainer = await ReadTrainerAsync(trainerId);
        if (trainer != null)
        {
            trainer.Pokemons.Add(pokemon);
            pokemon.Trainer = trainer;
            await _db.SaveChangesAsync();
        }
        return pokemon;
    }

    // UpdateTrainerAsync requires both trainerId and a trainer object,
    // if trainer works, it rquires the trainerId to get the old trainer 
    // and sets as trainerToUpdate, and then sets the new trainer object
    // to the old traineerToUpdate.

    public async Task UpdateTrainerAsync(int trainerId, Trainer trainer)
    {
        if (trainer != null)
        {
            var trainerToUpdate = await ReadTrainerAsync(trainerId);
            if(trainerToUpdate != null)
            {
                trainerToUpdate.Name = trainer.Name;
                trainerToUpdate.Money = trainer.Money;
                await _db.SaveChangesAsync();
            }
        }

    }

    // DeleteTrainerAsync requires trainerId, and if found
    // removes it from the trainers.

    public async Task DeleteTrainerAsync(int trainerId)
    {
        var trainerToRemove = await ReadTrainerAsync(trainerId);
        if (trainerToRemove != null)
        {
            _db.Trainers.Remove(trainerToRemove);
            await _db.SaveChangesAsync();
        }
    }

    // DeletePokemonAsync requires both trainerId, and pokemonId,
    // since in order to delete a pokemon you need to find the associated
    // trainer, and then if found, removes the pokemon.

    public async Task DeletePokemonAsync(int trainerId, int pokemonId)
    {
        var trainer = await ReadTrainerAsync(trainerId);
        if (trainer != null)
        {
            var pokemon = trainer.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
            if(pokemon != null)
            {
                trainer.Pokemons.Remove(pokemon);
                await _db.SaveChangesAsync();
            }
        }
    }

    // UpdatePokemonAsync requires both trainerId, as well as a pokemon object.
    // Similar to updating a trainer, but the way you find the pokemon to update
    // is to go through the trainer list of pokemon, and finding the one with the
    // corresponding id.

    public async Task UpdatePokemonAsync(int trainerId, Pokemon pokemon)
    {
        var trainer = await ReadTrainerAsync(trainerId);
        if (trainer != null)
        {
            var pokemonToUpdate = trainer.Pokemons.FirstOrDefault(p => p.Id == pokemon.Id);
            if (pokemonToUpdate != null)
            {
                pokemonToUpdate.Name = pokemon.Name;
                pokemonToUpdate.Level = pokemon.Level;
                pokemonToUpdate.Gender = pokemon.Gender;
                pokemonToUpdate.DexNumber = pokemon.DexNumber;
                pokemonToUpdate.Nature = pokemon.Nature;
                pokemonToUpdate.Ability = pokemon.Ability;
                pokemonToUpdate.Item = pokemon.Item;
                pokemonToUpdate.Type = pokemon.Type;
                pokemonToUpdate.SecondaryType = pokemon.SecondaryType;
                pokemonToUpdate.IsShiny = pokemon.IsShiny;
                pokemonToUpdate.HP = pokemon.HP;
                pokemonToUpdate.ATTACK = pokemon.ATTACK;
                pokemonToUpdate.DEFENCE = pokemon.DEFENCE;
                pokemonToUpdate.SPATK = pokemon.SPATK;
                pokemonToUpdate.SPDEF = pokemon.SPDEF;
                pokemonToUpdate.SPEED = pokemon.SPEED;
                await _db.SaveChangesAsync();
            }
        }
    }
}

