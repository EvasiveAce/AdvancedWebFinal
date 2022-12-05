using Final.Models.ViewModels;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers;

public class PokemonController : Controller
{
    private readonly ITrainerRepository _trainerRepo;

    public PokemonController(ITrainerRepository trainerRepo)
    {
        _trainerRepo = trainerRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    // (GET) Create while binding the prefix id to trainerId
    // Sees if trainer is null, if not create return view

    public async Task<IActionResult> Create([Bind(Prefix = "id")]int trainerId)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(trainerId);
        if (trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        ViewData["Trainer"] = trainer;
        return View();
    }

    // Does the actual create, requires both trainerId and pokemonVM
    // Requires trainerId since you cannot have a pokemon without a 
    // trainer.

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int trainerId, CreatePokemonVM pokemonVM)
    {
        if(ModelState.IsValid)
        {
            var pokemon = pokemonVM.GetPokemonInstance();
            await _trainerRepo.CreatePokemonAsync(trainerId, pokemon);
            return RedirectToAction("Details", "Trainer", new {id = trainerId});
        }
        return View(pokemonVM);
    }

    // (GET) Delete while binding the prefix id to trainerId and including pokemonId
    // Sees if trainer and pokemon is null, if not create return view of the new deleteVM model

    public async Task<IActionResult> Delete([Bind(Prefix ="id")]int trainerId, int pokemonId)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(trainerId);
        if (trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var pokemon = trainer.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
        if (pokemon == null)
        {
            return RedirectToAction("Details", "Trainer", new {id = trainerId});
        }
        var model = new DeletePokemonVM
        {
            Trainer = trainer,
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Gender = pokemon.Gender,
            DexNumber = pokemon.DexNumber,

            Nature = pokemon.Nature,
            Ability = pokemon.Ability,
            Type = pokemon.Type,
            SecondaryType = pokemon.SecondaryType,
            Item = pokemon.Item,

            HP = pokemon.HP,
            ATTACK = pokemon.ATTACK,
            DEFENCE = pokemon.DEFENCE,
            SPATK = pokemon.SPATK,
            SPDEF = pokemon.SPDEF,
            SPEED = pokemon.SPEED,

            IsShiny = pokemon.IsShiny,
        };
        return View(model);
    }

    // Deletes the actual Pokemon, requires both since you cannot
    // have a pokemon without a trainer, and it uses the trainerId
    // to find which pokemon needs to be deleted. 

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int trainerId)
    {
        await _trainerRepo.DeletePokemonAsync(trainerId, id);
        return RedirectToAction("Details", "Trainer", new {id = trainerId});
    }

    // (GET) Edit while binding the prefix id to trainerId and including pokemonId
    // Sees if trainer and pokemon is null, if not create return view of the new editVM model
    // (Note, almost identical to delete)

    public async Task<IActionResult> Edit([Bind(Prefix = "id")]int trainerId, int pokemonId)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(trainerId);
        if (trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var pokemon = trainer.Pokemons.FirstOrDefault(p => p.Id == pokemonId);
        if(pokemon == null)
        {
            return RedirectToAction("Details", "Trainer", new {id=trainerId});
        }
        var model = new EditPokemonVM
        {
            Trainer = trainer,
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Gender = pokemon.Gender,
            DexNumber = pokemon.DexNumber,
            Nature = pokemon.Nature,
            Ability = pokemon.Ability,
            Type = pokemon.Type,
            SecondaryType = pokemon.SecondaryType,
            Item = pokemon.Item,
            HP = pokemon.HP,
            ATTACK = pokemon.ATTACK,
            DEFENCE = pokemon.DEFENCE,
            SPATK = pokemon.SPATK,
            SPDEF = pokemon.SPDEF,
            SPEED = pokemon.SPEED,
            IsShiny = pokemon.IsShiny
        };
        return View(model);
    }

    // Edits the actual Pokemon, requires both since you cannot
    // have a pokemon without a trainer, and it uses the trainerId
    // to find which pokemon needs to be edited. 

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int trainerId, EditPokemonVM pokemonVM)
    {
        if (ModelState.IsValid)
        {
            var pokemon = pokemonVM.GetPokemonInstance();
            await _trainerRepo.UpdatePokemonAsync(trainerId, pokemon);
            return RedirectToAction("Details", "Pokemon", new {id=pokemon.Id});
        }
        return RedirectToAction("Details", "Trainer", new { id = trainerId });
    }

    // Shows the details of a selected pokemon. both trainerId and pokemonId is
    // used here, since you need to include the trainer since it is a detail of
    // a pokemon. Similar to Delete/Edit with the model.

    public async Task<IActionResult> Details(int id, int pokemonId)
    {
        var pokemon = await _trainerRepo.ReadPokemonAsync(pokemonId);
        if(pokemon == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var trainer = await _trainerRepo.ReadTrainerAsync(id);
        if(trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var model = new DetailsPokemonVM
        {
            Trainer = trainer,
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level,
            Gender = pokemon.Gender,
            DexNumber = pokemon.DexNumber,

            Nature = pokemon.Nature,
            Ability = pokemon.Ability,
            Type = pokemon.Type,
            SecondaryType = pokemon.SecondaryType,
            Item = pokemon.Item,

            HP = pokemon.HP,
            ATTACK = pokemon.ATTACK,
            DEFENCE = pokemon.DEFENCE,
            SPATK = pokemon.SPATK,
            SPDEF = pokemon.SPDEF,
            SPEED = pokemon.SPEED,

            IsShiny = pokemon.IsShiny,
        };
        return View(model);
    }
}

