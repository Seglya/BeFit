using System;
using System.Collections;
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
        private readonly IExerciseRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMusclesRepository _muscleRepository;
        private readonly IGroupOfMusclesRepository _groupOfMusclesRepository;


        public ExercisesController(IExerciseRepository repository, IHostingEnvironment hostingEvironment,
            IMusclesRepository muscleRepository, IGroupOfMusclesRepository groupOfMusclesRepository)
        {
            // _context = context;
            _repository = repository;
            _hostingEnvironment = hostingEvironment;
            _muscleRepository = muscleRepository;
            _groupOfMusclesRepository = groupOfMusclesRepository;

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
                item.Muscle = await _muscleRepository.GetMuscleAsync(item.MuscleID);
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
            CreateExerciseViewModel viewModel = new CreateExerciseViewModel
            {
                AllMuscles = _muscleRepository.Muscles,

            };

            return View(viewModel);
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExerciseViewModel viewModel)
        {


            if (_repository.GetExercise(viewModel.Name) != null)
            {
                ModelState.AddModelError("", "Exercise with this name already exist!!!");
            }

            if (ModelState.IsValid)
            {
                List<int> listId=new List<int>();
                foreach (var formKey in this.Request.Form.Keys)
                {
                    int i;
                    if (int.TryParse(formKey, out i))
                    {
                        listId.Add(i);
                    }

                }

                int saving = await _repository.SaveExerciseAsync(viewModel, 0);
                if (saving != 0 & listId.Count()!=0)
                    {
                        foreach (var i in listId)
                        {
                            var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(saving, i + 1);

                        }
                        viewModel.MusclesId = _groupOfMusclesRepository.GroupsOfMusclesByExercise(saving).ToList();

                    }
                int saving1 = await _repository.SaveExerciseAsync(viewModel, saving);
                if (saving1 != 0)
                    return RedirectToAction("Details", new {id = saving1});
            }

        viewModel.AllMuscles=_muscleRepository.Muscles;
    
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
            }   foreach (var item in exercise.Muscles)
            {
                item.Muscle = await _muscleRepository.GetMuscleAsync(item.MuscleID);
            }
            CreateExerciseViewModel viewModel=new CreateExerciseViewModel
            {
                AllMuscles = _muscleRepository.Muscles,
                Name = exercise.Name,
                Description = exercise.Description,
                MusclesId= exercise.Muscles
            };
         
            if (exercise.ImageData!=null)
                viewModel.ImageData =exercise.ImageData.ToArray();
            return View(viewModel);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateExerciseViewModel viewModel)
        {
            if (id == 0)
            {
                return NotFound();
            }
            List<int> listId = new List<int>();
            foreach (var formKey in this.Request.Form.Keys)
            {
                int i;
                if (int.TryParse(formKey, out i))
                {
                    listId.Add(i);
                }

            }
           
                _groupOfMusclesRepository.DeleteGroupOfMuscle(_groupOfMusclesRepository.GroupsOfMuscles.ToList());

            if (listId.Count() != 0)
            {
                foreach (var i in listId)
                {
                    var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(id, i + 1);

                }
                viewModel.MusclesId = _groupOfMusclesRepository.GroupsOfMusclesByExercise(id).ToList();

            }
            int saving1 = await _repository.SaveExerciseAsync(viewModel, id);
            if (saving1 != 0)
                return RedirectToAction("Details", new {id = id});
             return View(viewModel);
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
