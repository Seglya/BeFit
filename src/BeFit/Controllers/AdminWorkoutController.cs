using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.ExerciseViewModels;
using BeFit.Models.WorkoutViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;

namespace BeFit.Controllers
{
    public class AdminWorkoutController: Controller

    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IFillingWorkoutRepository _fillingWorkoutRepository;



        public AdminWorkoutController(IExerciseRepository exerciseRepository, IHostingEnvironment hostingEvironment,
            IWorkoutRepository workoutRepository, ITagRepository tagRepository, IFillingWorkoutRepository fillingWorkoutRepository)
        {
            // _context = context;
            _exerciseRepository=exerciseRepository;
            _hostingEnvironment = hostingEvironment;
            _workoutRepository=workoutRepository;
            _tagRepository = tagRepository;
            _fillingWorkoutRepository = fillingWorkoutRepository;
        }

        // GET:Workout
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
            }
            ViewData["CurrentFilter"] = filter;
            var collWorkouts = _workoutRepository.WorkoutsByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collWorkouts = collWorkouts.OrderByDescending(s => s.Name);
                    break;
                default: collWorkouts = collWorkouts.OrderBy(s => s.Name); break;
            }
            int pageSize = 6;
            return View(PagerList<Workout>.Create(collWorkouts, page ?? 1, pageSize));
        }

        //GET: Workout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var workout = await _workoutRepository.GetWorkoutAsync(id);
            if (workout == null)
                return NotFound();
            return View(workout);
        }



        //  GET: Workout/Create
        public IActionResult Create()
        {
            var viewModel = new WorkoutViewModel
            {
             
                 AllExercises = _exerciseRepository.Exercises.ToList(),
               AllTags = _tagRepository.Tags
            };
            ViewData["chosenExerises[]"] = null;
            ViewData["inputSets[]"] = null;
            ViewData["inputMinRep[]"] = null;
            ViewData["inputMaxRep[]"] = null;
            ViewData["CurrentTag"] = null;
            ViewData["Title"] = "Create";
            return View(viewModel);
        }

       // GET: Workout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var workout = await _workoutRepository.GetWorkoutAsync(id);
            if (workout == null)
                return NotFound();
            var viewModel = new WorkoutViewModel
            {

                AllExercises = _exerciseRepository.Exercises.ToList(),
                AllTags = _tagRepository.Tags,
                Name = workout.Name,
                Description = workout.Description
                
            };
            ViewData["Title"] = "Edit";
            string[] arryExercises=new string[workout.Exercises.Count];
            string[] arrySets = new string[workout.Exercises.Count];
            string[] arryMin = new string[workout.Exercises.Count];
            string[] arryMax = new string[workout.Exercises.Count];
            int e = 1;
            string workTag=null;
            foreach (var tag in viewModel.AllTags)
            {
                if (tag.TagID == workout.TagID)
                    workTag = e.ToString();
                e++;
            }
            e = 0;
            foreach (var fillingWorkout in workout.Exercises)
            {
                arryExercises[e] = fillingWorkout.Exercise.Name;
                arrySets[e] = fillingWorkout.Sets.ToString();
                arryMin[e] = fillingWorkout.RepeatMin.ToString();
                arryMax[e] = fillingWorkout.RepeatMax.ToString();
                e++;

            }
            ViewData["chosenExerises[]"] = arryExercises;
            ViewData["inputSets[]"] = arrySets;
            ViewData["inputMinRep[]"] = arryMin;
            ViewData["inputMaxRep[]"] = arryMax;
            ViewData["CurrentTag"] = workTag;
            return View("Create",viewModel);

        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkoutViewModel viewModel, string tagIndex, string[] Exercises, string[] Sets, string[] MinRep, string[] MaxRep)
        {
            ViewData["chosenExerises[]"] = Exercises;
            ViewData["inputSets[]"] = Sets;
            ViewData["inputMinRep[]"] = MinRep;
            ViewData["inputMaxRep[]"] = MaxRep;
            ViewData["CurrentTag"] = tagIndex;
            if (tagIndex.IsNullOrWhiteSpace())
                ModelState.AddModelError("", "Chose the tag!!!");
            if (Exercises.Length != Sets.Length || Sets.Length != MinRep.Length || MinRep.Length != MaxRep.Length)
                ModelState.AddModelError("", "All Fields of Exercise should be complete!!!");
            else
            {

                for (int i = 0; i < MinRep.Length; i++)
                {
                    int min, max, set;
                    if (!int.TryParse(MinRep[i], out min))
                    { ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of Min repeats should be a digit!!!"); }

                    if (!int.TryParse(MaxRep[i], out max))
                    { ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of Max repeats should be a digit!!!"); }
                    else if (max < min)
                    { ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of Max Repeat should be bigger then Min Reapeat or equally !!!"); }
                    if (!int.TryParse(Sets[i], out set))
                    {
                        ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of Sets should be a digit!!!");
                    }
                    else
                   if (max < 1 || min < 1 || set < 1)
                    { ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of the fields \"Sets\", \"Min Reapeat\", \"Max Repeat\" should be more then \"0\"!!!"); }

                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int t = 1;
                    foreach (var tag in _tagRepository.Tags)
                    {
                        if (t == int.Parse(tagIndex))
                            viewModel.TagId = tag.TagID;
                        t++;
                    }
                    ViewData["Title"] = "Create";
                    if (id != 0)
                    {
                        ViewData["Title"] = "Edit";
                        var workEx = _workoutRepository.GetWorkoutAsync(id).Result.Exercises;
                    
                    _fillingWorkoutRepository.DeleteFillingWorkout(workEx);
                }
                var saving = await _workoutRepository.SaveWorkoutAsync(viewModel, id);
                    for (int i = 0; i < Exercises.Length; i++)
                    {
                       
                        var gr = await _fillingWorkoutRepository.NewFillingWorkoutAsync(saving, _exerciseRepository.GetExercise(Exercises[i]), int.Parse(Sets[i]), int.Parse(MinRep[i]), int.Parse(MaxRep[i]));
                    }
                    viewModel.Exercises = _fillingWorkoutRepository.FillingWorkoutByWorkouts(id).ToList();
                   
                    if (saving != 0)
                        return RedirectToAction("Details", new { id = saving });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            viewModel.AllExercises = _exerciseRepository.Exercises.ToList();
            viewModel.AllTags = _tagRepository.Tags;
            return View("Create",viewModel);
        }

        // GET: Workout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var workout = await _workoutRepository.GetWorkoutAsync(id);
            if (workout == null)
                return NotFound();

            return View(workout);
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var exercise = _workoutRepository.DeleteWorkout(id);
            }
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
