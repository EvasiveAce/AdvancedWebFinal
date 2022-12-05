using Final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    // What the add/editing/removing/details comes from for the database
    // Trainers will be stored in Trainers, and Pokemon will be stored in
    // Pokemons.
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();
}

