using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;


        public ActorsController(MoviesContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Actors
        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Actors.Select(m => new ActorViewModel
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth
            }).ToList());
        }

        // GET: Actors/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.Actors
                .Include(a => a.Movies)
                .ThenInclude(m => m.Movie)
                .Where(m => m.Id == id)
                .Select(m => new ActorViewModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    DateOfBirth = m.DateOfBirth,
                    Movies = m.Movies.Select(r => r.Movie).ToList()
                }).FirstOrDefault();


            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var actor = new InputActorViewModel();

            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeIntervalActor]
        [MinLengthAttribute]
        public IActionResult Create([Bind("FirstName,LastName,DateOfBirth")] InputActorViewModel inputModel)
        {
            var newActor = new Actor
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                DateOfBirth = inputModel.DateOfBirth,
                // Movies = inputModel.ActorsMovies
            };
            if (ModelState.IsValid)
            {
                _context.Actors.Add(newActor);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        [HttpGet]
        [MinLengthAttribute]
        // GET: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.Actors
                .Include(a => a.Movies)
                .ThenInclude(m => m.Movie)
                .Where(m => m.Id == id)
                .Select(m => new EditActorViewModel()
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    DateOfBirth = m.DateOfBirth,
                    Movies = m.Movies.Select(r => r.Movie).ToList()
                }).FirstOrDefault();

            if (editModel == null)
            {
                return NotFound();
            }

            editModel.AllMovies = _context.Movies.ToList();

            return View(editModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeIntervalActor]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,DateOfBirth")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = new Actor
                    {
                        Id = id,
                        FirstName = editModel.FirstName,
                        LastName = editModel.LastName,
                        DateOfBirth = editModel.DateOfBirth
                    };

                    _context.Update(actor);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!ActorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }

        [HttpGet]
        // GET: Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _context.Actors.Where(m => m.Id == id).Select(m => new DeleteActorViewModel
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth
            }).FirstOrDefault();

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _context.Actors.Find(id);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            _logger.LogError($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}