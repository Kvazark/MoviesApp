using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MoviesContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any())
                {
                    context.SaveChanges();
                }

                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole {Name = "Admin"}).Wait();
                }

                if (userManager.FindByEmailAsync("admin@example.com").Result == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        FirstName = "Super",
                        LastName = "Admin"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M
                        },
                        new Movie
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M
                        },
                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M
                        },
                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(
                        new Actor
                        {
                            FirstName = "Dino",
                            LastName = "Paul Crocetti",
                            DateOfBirth = DateTime.Parse("1917-06-07")
                        },
                        new Actor
                        {
                            FirstName = "John",
                            LastName = "Wayne",
                            DateOfBirth = DateTime.Parse("1907-05-26")
                        },
                        new Actor
                        {
                            FirstName = "Harold",
                            LastName = "Ramis",
                            DateOfBirth = DateTime.Parse("1944-11-21")
                        },
                        new Actor
                        {
                            FirstName = "Anne",
                            LastName = "Hathaway",
                            DateOfBirth = DateTime.Parse("1982-11-12")
                        },
                        new Actor
                        {
                            FirstName = " Casey",
                            LastName = "Affleck",
                            DateOfBirth = DateTime.Parse("1975-08-19")
                        });
                    context.SaveChanges();
                }

                if (!context.ActorsMovies.Any())
                {
                    context.ActorsMovies.AddRange(
                        new ActorsMovies
                        {
                            ActorId = 3, MovieId = 5
                        },
                        new ActorsMovies
                        {
                            ActorId = 3, MovieId = 7
                        },
                        new ActorsMovies
                        {
                            ActorId = 22, MovieId = 4
                        },
                        new ActorsMovies
                        {
                            ActorId = 24, MovieId = 5
                        },
                        new ActorsMovies
                        {
                            ActorId = 23, MovieId = 4
                        },
                        new ActorsMovies
                        {
                            ActorId = 1, MovieId = 2
                        },
                        new ActorsMovies
                        {
                            ActorId = 14, MovieId = 5
                        });

                    context.SaveChanges();
                }
            }
        }
    }
}