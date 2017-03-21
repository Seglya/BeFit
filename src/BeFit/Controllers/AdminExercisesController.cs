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
    public class AdminExercisesController : Controller
    {
        // private readonly BeFitDbContext _context;
        private readonly IExerciseRepository _repository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMusclesRepository _muscleRepository;
        private readonly IGroupOfMusclesRepository _groupOfMusclesRepository;


        public AdminExercisesController(IExerciseRepository repository, IHostingEnvironment hostingEvironment,
            IMusclesRepository muscleRepository, IGroupOfMusclesRepository groupOfMusclesRepository)
        {
            
            _repository = repository;
            _hostingEnvironment = hostingEvironment;
            _muscleRepository = muscleRepository;
            _groupOfMusclesRepository = groupOfMusclesRepository;
        }

        // GET: Exercises
        public IActionResult Index(string sortOrder,string pageSize, string filter, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!string.IsNullOrEmpty(pageSize))
                ViewData["SizePage"] = pageSize;
            else if (string.IsNullOrEmpty(pageSize) & string.IsNullOrEmpty((string)ViewData["SizePage"]))
            {
                pageSize = (string)ViewData["SizePage"];
            }


            int sizeOfPage;
            if (int.TryParse(pageSize, out sizeOfPage) == false)
                sizeOfPage = 12;
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
            return View(PagerList<Exercise>.Create(collExercises, page ?? 1, sizeOfPage));
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
        public async Task<IActionResult> Create(CreateExerciseViewModel viewModel, string [] musclesIndex)
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
                        var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(saving, _muscleRepository.GetMuscleByIndex(i));
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
            int i = 0;
            string[] musclesIndex = new string[viewModel.Muscles.Count];
            foreach (var muscle in viewModel.Muscles)
            {
                musclesIndex[i] = _muscleRepository.GetIndex(muscle.Muscle).ToString();
                i++;
            }
            ViewBag.muscles = musclesIndex;
            if (exercise.ImageData != null)
                viewModel.ImageData = exercise.ImageData.ToArray();
            return View(viewModel);

        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateExerciseViewModel viewModel, string[] musclesIndex)
        {
            
            if (id == 0)
                return NotFound();
            var listId = new List<int>();
           
            foreach (var s in musclesIndex)
            {
                
            
                    listId.Add(int.Parse(s));
            }
            if (listId.Count == 0)
                ModelState.AddModelError("", "Choose at least one group of muscle");
            try { 
            if (ModelState.IsValid)
            {
                _groupOfMusclesRepository.DeleteGroupOfMuscle(_groupOfMusclesRepository.GroupsOfMusclesByExercise(id).ToList());
                foreach (var i in listId)
                {
                    var gr = await _groupOfMusclesRepository.NewGroupOfMusclesAsync(id, _muscleRepository.GetMuscleByIndex(i) );
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

        
    }
}
