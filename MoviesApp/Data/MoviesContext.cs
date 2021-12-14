using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public class MoviesContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesContext (DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorsMovies>(entity =>
                entity.HasKey(sc => new {sc.ActorId, sc.MovieId}));
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorsMovies> ActorsMovies { get; set; }
    }
}