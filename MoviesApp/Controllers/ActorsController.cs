using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;


        public ActorsController(IActorService service, ILogger<HomeController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Actors
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<ActorDto>, IEnumerable<ActorViewModel>>(_service.GetAllActors());
            return View(actors);
        }

        // GET: Actors/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var viewModel = _mapper.Map<ActorViewModel>(_service.GetActor((int) id));


            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        [AgeIntervalActor]
        [MinLengthAttribute]
        [EnsureReleaseDateBeforeNow]
        public IActionResult Create([Bind("FirstName,LastName,DateOfBirth")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddActor(_mapper.Map<ActorDto>(inputModel));
                return RedirectToAction(nameof(Index));
                
            }

            return View(inputModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] 
        [MinLengthAttribute]
        // GET: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<EditActorViewModel>(_service.GetActor((int) id));

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        // POST: Actors/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        [AgeIntervalActor]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,DateOfBirth")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var actor = _mapper.Map<ActorDto>(editModel);
                actor.Id = id;
                
                var result = _service.UpdateActor(actor);
                
                if (result == null)
                {
                    return NotFound();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(editModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] 
        // GET: Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<DeleteActorViewModel>(_service.GetActor((int) id));

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _service.DeleteActor(id);
            if (actor==null)
            {
                return NotFound();
            }
            _logger.LogTrace($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
        
    }
}