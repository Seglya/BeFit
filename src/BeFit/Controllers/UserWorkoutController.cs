using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class UserWorkoutController : Controller
    {
        private IWorkoutRepository _workoutRepository;
        private IOneDayWorkoutRepository _oneDayWorkoutRepository;
        private ICardioRepository _cardioRepository;


        public UserWorkoutController(IWorkoutRepository workoutRepository, IOneDayWorkoutRepository oneDayWorkoutRepository, ICardioRepository cardioRepository)
        {
            _workoutRepository = workoutRepository;
            _oneDayWorkoutRepository = oneDayWorkoutRepository;
            _cardioRepository = cardioRepository;
        }
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            var oneDayWorkouts = _oneDayWorkoutRepository.AllOneDayWorkoutsByUserId(id);
            return View(oneDayWorkouts);
        }

        public IActionResult Create(int id)
        {
            return View();
        }
        private void PopulateCardioDropDownList(object selectedCardio = null)
        {
            var cardio = _cardioRepository.Cardios;
            ViewBag.FoodID = new SelectList(cardio, "CardioID", "Name", selectedCardio);
        }
    }
}
