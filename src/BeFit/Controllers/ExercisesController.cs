using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models.ExerciseViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.DotNet.PlatformAbstractions;
using BeFit.Models;
using Microsoft.AspNetCore.Hosting;




namespace BeFit.Controllers
{
    public class ExercisesController : Controller
    {
       // private readonly BeFitDbContext _context;
        private IExerciseRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ExercisesController( IExerciseRepository repository, IHostingEnvironment hostingEvironment)
        {
           // _context = context;
            _repository = repository;
            _hostingEnvironment = hostingEvironment;
            
        }

        // GET: Exercises
        public IActionResult Index()
        {
            return View(_repository.Exercises);
        }

         //GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            foreach (var item in exercise.Muscles)
            {
                item.Muscle=await _repository.GetMuscleAsync(item.MuscleID);
            }
            return View(exercise);
        }
        public async Task<FileContentResult> GetImageAsync(int id)
        {
                var exer = await _repository.GetExerciseAsync(id);
            if (exer != null)
            {
                return File(exer.ImageData, exer.ImageMimeType);
            }
            return null;
        }

       //  GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExerciseViewModel viewModel)
        {


            if (_repository.GetExercise(viewModel.Name) != null)
            { ModelState.AddModelError("", "Exercise with this name already exist!!!"); }

            if (ModelState.IsValid)
            {
            using (var memoryStream = new MemoryStream())
            {
                var exercise = new Exercise
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,

                };
                    if (viewModel.image != null)
                    {
                        await viewModel.image.CopyToAsync(memoryStream);
                        exercise.ImageData = memoryStream.ToArray();

                        exercise.ImageMimeType = viewModel.image.ContentType;
                    }
            
             bool saving = _repository.SaveExercise(exercise);
               if(saving ==true)
                return RedirectToAction("Details", new { id = _repository.GetExercise(exercise.Name).ExerciseID });
               
            }}
            return View(viewModel);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
           
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateExerciseViewModel viewModel)
        {
            if (id ==0)
            {
                return NotFound();
            }
            Exercise exercise;
            
            using (var memoryStream = new MemoryStream())
            {
                exercise = new Exercise
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ExerciseID = id,
                };
                if (viewModel.image != null) { 
                await viewModel.image.CopyToAsync(memoryStream);
                exercise.ImageData = memoryStream.ToArray();
                exercise.ImageMimeType = viewModel.image.ContentType;}if (ModelState.IsValid)
            {
            
           

                if (_repository.SaveExercise(exercise))
                {

                    return RedirectToAction("Details", new { id = id });
                }
             
                return RedirectToAction("Index");}
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var exercise = _repository.DeleteExercise(id);
          
            return RedirectToAction("Index");
        }

        private bool ExerciseExists(int id)
        {
            return _repository.Exercises.Any(e => e.ExerciseID == id);
        }
        public async Task<FileContentResult> GetImage(int exId)
        {
            var exercise = await _repository.GetExerciseAsync(exId);
            if (ExerciseExists(exId))
                return File(exercise.ImageData, exercise.ImageMimeType);
            else
                return null;
        }
    }
}
