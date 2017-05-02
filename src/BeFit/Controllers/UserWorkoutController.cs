using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using BeFit.Models.WorkoutViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class UserWorkoutController : Controller
    {
        private readonly ICardioRepository _cardioRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IFillingWorkoutRepository _fillingWorkoutRepository;
        private readonly IOneDayWorkoutRepository _oneDayWorkoutRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IWorkoutRepository _workoutRepository;


        public UserWorkoutController(ITagRepository tagRepository, IFillingWorkoutRepository fillingWorkoutRepository,
            IWorkoutRepository workoutRepository, IOneDayWorkoutRepository oneDayWorkoutRepository,
            ICardioRepository cardioRepository, IExerciseRepository exerciseRepository)
        {
            _workoutRepository = workoutRepository;
            _oneDayWorkoutRepository = oneDayWorkoutRepository;
            _cardioRepository = cardioRepository;
            _exerciseRepository = exerciseRepository;
            _fillingWorkoutRepository = fillingWorkoutRepository;
            _tagRepository = tagRepository;
        }

        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            ViewData["user"] = id;
            var oneDayWorkouts = _oneDayWorkoutRepository.AllOneDayWorkoutsByUserId(id);
            return View(oneDayWorkouts);
        }

        //GET: /UserWorkout/Create/id
        public IActionResult Create(int id, int? oneDayWorkoutId)
        {
            if (oneDayWorkoutId == null)
                oneDayWorkoutId = 0;
            var oneDayWorkout = _oneDayWorkoutRepository.GetOneDayWorkoutByIdAsync((int) oneDayWorkoutId).Result;
            var viewModel = new OneDayWorkoutViewModel
            {
                AppUserID = id,
                Date = DateTime.Today.Date,
                UserWorkoutViewModel = new UserWorkoutViewModel()
            };
            var workoutViewModel = new UserWorkoutViewModel();
            if (oneDayWorkout != null)
            {
                if (oneDayWorkout.WorkoutID != null)
                {
                    workoutViewModel.PersonWorkout = true;
                    workoutViewModel.Exercises = new List<UserExerciseViewModel>();
                    foreach (var workoutExercise in oneDayWorkout.Workout.Exercises)
                    {
                        var model = new UserExerciseViewModel
                        {
                            NameExercise = workoutExercise.Exercise.Name,
                            ExerciseID = workoutExercise.ExerciseID,
                            Repeat = workoutExercise.RepeatMax,
                            Sets = workoutExercise.Sets,
                            WorkoutID = workoutExercise.WorkoutID
                        };
                        workoutViewModel.Exercises.Add(model);
                    }
                    
                }
                else
                {
                    viewModel.WorkoutID = 0;
                }
                viewModel.UserWorkoutViewModel = workoutViewModel;
                viewModel.Date = oneDayWorkout.Date;
                viewModel.CardioID = oneDayWorkout.CardioID;
                viewModel.CardioName = oneDayWorkout.Cardio.Name;
                viewModel.Duration = oneDayWorkout.Duration;
                viewModel.WorkoutID = oneDayWorkout.WorkoutID;
                viewModel.AppUserID = oneDayWorkout.AppUserID;
                PopulateCardioDropDownList(oneDayWorkout.Cardio);
            }
            else
            {
                PopulateCardioDropDownList();
            }
            PopulateExerciseDropDownList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, int? oneDayWorkoutId, OneDayWorkoutViewModel viewModel, string[] Sets,
            string[] Reapeats,
            string[] Exercise)
        {
            if ((Sets.Length != Reapeats.Length) & (Reapeats.Length != Exercise.Length))
                ModelState.AddModelError("", "All fields should be complete!!!");
            else
                for (var i = 0; i < Sets.Length; i++)
                {
                    int exer, rep, set;
                    if (!int.TryParse(Exercise[i], out exer))
                        ModelState.AddModelError("",
                            "Exercise №" + (i + 1) + "Exercise not selected!!!");
                    if (!int.TryParse(Reapeats[i], out rep))
                        ModelState.AddModelError("",
                            "Exercise №" + (i + 1) + "Value of Repeats should be a digit!!!");
                    if (!int.TryParse(Sets[i], out set))
                        ModelState.AddModelError("", "Exercise №" + (i + 1) + "Value of Sets should be a digit!!!");
                    else if (rep < 1 || set < 1)
                        ModelState.AddModelError("",
                            "Exercise №" + (i + 1) +
                            "Value of the fields \"Sets\", \"Repeat\" should be more then \"0\"!!!");
                }
            var dates = _oneDayWorkoutRepository.AllOneDayWorkoutsByUserId(viewModel.AppUserID).Where(d=>d.Date==viewModel.Date);
            if (dates.Any())
            {
                ModelState.AddModelError("", errorMessage: "Measurement on this date already exist!");
            }
         
                if (ModelState.IsValid)
                try
                {
                    if (viewModel.WorkoutID == null)
                        viewModel.WorkoutID = 0;
                    viewModel.CardioName = _cardioRepository.GetCardioAsync(viewModel.CardioID).Result.Name;
                    viewModel.UserWorkoutViewModel.Exercises = new List<UserExerciseViewModel>();
                    if (oneDayWorkoutId == null)
                        oneDayWorkoutId = 0;
                    else
                    {
                        _fillingWorkoutRepository.DeleteFillingWorkout(
                            _fillingWorkoutRepository.FillingWorkoutByWorkouts((int) oneDayWorkoutId).ToList());
                    }
                    var workout =
                        _workoutRepository.SaveWorkoutAsync(
                            new WorkoutViewModel
                            {
                                Name = viewModel.UserWorkoutViewModel.Name,
                                PersonWorkout = true,
                                TagId = _tagRepository.TagByFilter("Strength").First().TagID
                            }, (int) viewModel.WorkoutID);
                    viewModel.WorkoutID = workout;
                    for (var i = 0; i < Exercise.Length; i++)
                    {
                        var gr = _fillingWorkoutRepository.NewFillingWorkoutAsync(workout,
                            _exerciseRepository.GetExerciseAsync(int.Parse(Exercise[i])).Result, int.Parse(Sets[i]),
                            int.Parse(Reapeats[i]),
                            int.Parse(Reapeats[i])).Result;
                        viewModel.UserWorkoutViewModel.Exercises.Add(new UserExerciseViewModel
                        {
                            ExerciseID = gr.ExerciseID,
                            NameExercise = gr.Exercise.Name,
                            Sets = gr.Sets,
                            WorkoutID = gr.WorkoutID,
                            Repeat = gr.RepeatMax
                        });

                    }
                    var sav =
                        _oneDayWorkoutRepository.AddOrEditOneDayWorkoutAsync((int) oneDayWorkoutId, viewModel).Result;
                    if (sav != null)
                        return RedirectToAction("Index", new { id = id})
            ;
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists " +
                                                 "see your system administrator.");
                }

            return View(viewModel);
        }

        public IActionResult Detail(int oneDayWorkoutId)
        {
            var workouts = _oneDayWorkoutRepository.GetOneDayWorkoutByIdAsync(oneDayWorkoutId).Result;
            var listExercise = new List<UserExerciseViewModel>();
            foreach (var exercise in workouts.Workout.Exercises)
            {
                listExercise.Add(new UserExerciseViewModel {ExerciseID = exercise.ExerciseID, WorkoutID = exercise.WorkoutID, Sets = exercise.Sets, Repeat = exercise.RepeatMax, NameExercise = exercise.Exercise.Name});
            }
            ViewData["calories"] = workouts.Cardio.CalPerHour * workouts.Duration;
            ViewData["oneDayId"] = oneDayWorkoutId;
            var viewModel=new OneDayWorkoutViewModel {AppUserID = workouts.AppUserID, CardioID = workouts.CardioID, CardioName = workouts.Cardio.Name, Date = workouts.Date, Duration = workouts.Duration, WorkoutID = workouts.WorkoutID, UserWorkoutViewModel = new UserWorkoutViewModel {Name = workouts.Workout.Name, Exercises  = listExercise} };
            return View(viewModel);
        }

        // GET: UserWorkout/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var workout = await _oneDayWorkoutRepository.GetOneDayWorkoutByIdAsync(id);
        if (workout == null)
                return NotFound();
            ViewData["oneDayId"] = id;
            var viewModel = new OneDayWorkoutViewModel { AppUserID = workout.AppUserID, CardioID = workout.CardioID, CardioName = workout.Cardio.Name, Date = workout.Date, Duration = workout.Duration, WorkoutID = workout.WorkoutID, UserWorkoutViewModel = new UserWorkoutViewModel { Name = workout.Workout.Name } };
            return View(viewModel);
        }

        // POST: UserWorkout/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int user)
        {
            
            try
            {
                _oneDayWorkoutRepository.DeleteOneDayWorkout(id);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Delete failed. Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return RedirectToAction("Index", new {id= user });
        }
        private void PopulateCardioDropDownList(object selectedCardio = null)
        {
            var cardio = _cardioRepository.Cardios;
            ViewBag.CardioID = new SelectList(cardio, "CardioID", "Name", selectedCardio);
        }

        private void PopulateExerciseDropDownList(object selectedExercise = null)
        {
            var exercise = _exerciseRepository.Exercises;
            ViewBag.ExerciseID = new SelectList(exercise, "ExerciseID", "Name", selectedExercise);
        }
    }
}