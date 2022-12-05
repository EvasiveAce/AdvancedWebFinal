using Final.Models.Entities;
using Final.Models.ViewModels;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers;

public class TrainerController : Controller
{
    private readonly ITrainerRepository _trainerRepo;

    // Simply allows to use the _trainerRepo

    public TrainerController(ITrainerRepository trainerRepo)
    {
        _trainerRepo = trainerRepo;
    }

    //public async Task<IActionResult> Index()
    //{
    //    var allTrainers = await _trainerRepo.ReadAllAsync();
    //    var model = allTrainers.Select(t =>
    //    new TrainerDetailsVM
    //    {
    //        Id = t.Id,
    //        Name = t.Name,
    //        Money = t.Money,
    //        NumberOfPokemon = t.Pokemons.Count
    //    });
    //    return View(model);
    //}

    // The trainer index shows every trainer in a list

    public async Task<IActionResult> Index()
    {
        return View(await _trainerRepo.ReadAllAsync());
    }

    // Details shows a single trainer, which includes the
    // detailsVM as well as a count of the NumberOfPokemon.
    // Requires the trainerId to find. 

    public async Task<IActionResult> Details(int id)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(id);
        if (trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var model = new DetailsTrainerVM
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Money = trainer.Money,
            NumberOfPokemon = trainer.Pokemons.Count,
            Pokemons = trainer.Pokemons
        };
        return View(model);
    }
    
    // A modal-specific task. Takes trainerId, andd basically 
    // shows the _trainerRow for the trainer, basically includes it
    // without having to include it in the view.

    public async Task<IActionResult> TrainerRow(int id)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(id);
        return PartialView("~/Views/Trainer/_TrainerRow.cshtml", trainer);
    }

    // Post for Create, takes in the trainer that is being created, 
    // which is from the modal. If successful return success, if not
    // failed.

    [HttpPost]
    public async Task<IActionResult> CreateAjax(Trainer trainer)
    {
        if (ModelState.IsValid)
        {
            await _trainerRepo.CreateTrainerAsync(trainer);
            return Json(new { trainerId = trainer.Id, message = "success" });
        }
        return Json("Failed");
    }

    // (GET) Edit for trainer, requires trainerId for the readTrainer,
    // makes it a EditTrainerVM model.

    public async Task<IActionResult> Edit(int id)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(id);
        if(trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var model = new EditTrainerVM
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Money = trainer.Money,
            Pokemons = trainer.Pokemons
        };
        return View(model);
    }

    // The post for Edit, requires trainerId as well as the just made 
    // EditTrainerVM. EditTrainerVM for the instance, and id for update.

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int trainerId, EditTrainerVM trainerVM)
    {
        if (ModelState.IsValid)
        {
            var trainer = trainerVM.GetTrainerInstance();

            await _trainerRepo.UpdateTrainerAsync(trainerId, trainer);
            return RedirectToAction("Details", "Trainer", new { id = trainerId });
        }
        return RedirectToAction("Index", "Trainer");
    }

    // (GET) Delete for trainer, requires trainerId for the readTrainer,
    // makes it a DeleteTrainerVM model.

    public async Task<IActionResult> Delete(int id)
    {
        var trainer = await _trainerRepo.ReadTrainerAsync(id);
        if(trainer == null)
        {
            return RedirectToAction("Index", "Trainer");
        }
        var model = new DeleteTrainerVM
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Money = trainer.Money,
            Pokemons = trainer.Pokemons
        };
        return View(model);
    }

    // Post for Delete, takes trainerId. If found,
    // awaits the DeleteTrainerAsync and returns.

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int trainerId)
    {
        await _trainerRepo.DeleteTrainerAsync(trainerId);
        return RedirectToAction("Index", "Trainer");
    }
}
