using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.ExerciseViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Index(string sortOrder, string filter, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (filter != null)
            {
                page = 1;
            }
            else
            {
                filter = currentFilter;
            }ViewData["CurrentFilter"] = filter;
            var collExercises = _repository.ExercisesByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc": collExercises=collExercises.OrderByDescending(s => s.Name);
                    break;
                default: collExercises=collExercises.OrderBy(s => s.Name);break;
            }
            int pageSize = 6;
            return View(PagerList<Exercise>.Create(collExercises, page ?? 1, pageSize));
        }

        //GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
                return NotFound();
         return View(exercise);
        }

        public async Task<FileContentResult> GetImageAsync(int id)
        {
            var exer = await _repository.GetExerciseAsync(id);
            if (exer != null)
                return File(exer.ImageData, exer.ImageMimeType);
            return null;
        }

        //  GET: Exercises/Create
        public IActionResult Create()
        {
            var viewModel = new CreateExerciseViewModel
            {
                AllMuscles = _muscleRepository.Muscles
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
                ModelState.AddModelError("", "Exercise with this name already exist!!!");


            var listId = new List<int>();
            foreach (var formKey in Request.Form.Keys)
            {
                int i;
                if (int.TryParse(formKey, out i))
                    listId.Add(i);
            }
            if (listId.Count == 0)
                ModelState.AddModelError("", "Choose at least one group of muscle");
            try { 
            if (ModelState.IsValid)
            {
                var saving = await _repository.SaveExerciseAsync(viewModel, 0);
                if (saving != 0)
                {
                    foreach (var i in listId)
                    {
                        var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(saving, i + 1);
                    }
                    viewModel.Muscles = _groupOfMusclesRepository.GroupsOfMusclesByExercise(saving).ToList();
                }

                if (saving != 0)
                    return RedirectToAction("Details", new {id = saving});
            }
        }
        catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            viewModel.AllMuscles = _muscleRepository.Muscles;

            return View(viewModel);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
                return NotFound();
            var viewModel = new CreateExerciseViewModel
            {
                AllMuscles = _muscleRepository.Muscles,
                Name = exercise.Name,
                Description = exercise.Description,
                Muscles = exercise.Muscles
            };

            if (exercise.ImageData != null)
                viewModel.ImageData = exercise.ImageData.ToArray();
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
                return NotFound();
            var listId = new List<int>();
            foreach (var formKey in Request.Form.Keys)
            {
                int i;
                if (int.TryParse(formKey, out i))
                    listId.Add(i);
            }
try { 
            if (listId.Count != 0)
            {
                _groupOfMusclesRepository.DeleteGroupOfMuscle(_groupOfMusclesRepository.GroupsOfMuscles.ToList());
                foreach (var i in listId)
                {
                    var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(id, i + 1);
                }
                viewModel.Muscles = _groupOfMusclesRepository.GroupsOfMusclesByExercise(id).ToList();
            }

            var saving1 = await _repository.SaveExerciseAsync(viewModel, id);
            if (saving1 != 0)
                return RedirectToAction("Details", new {id});
        }
        catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            viewModel.AllMuscles = _muscleRepository.Muscles;
            return View(viewModel);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var exercise = await _repository.GetExerciseAsync(id);
            if (exercise == null)
                return NotFound();

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try { 
            var exercise = _repository.DeleteExercise(id);}
             catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("Index");
        }

        private bool ExerciseExists(int id)
        {
            return _repository.Exercises.Any(e => e.ExerciseID == id);
        }

      
    }
}
