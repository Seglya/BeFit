using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class ChartsController : Controller
    {
        private IOneDayWorkoutRepository _oneDayWorkoutRepository;
        private IFillMeasurementRepository _fillMeasurementRepository;
        private IOneDayFoodRepository _oneDayFoodRepository;

        public ChartsController(IOneDayWorkoutRepository oneDayWorkoutRepository, IFillMeasurementRepository fillMeasurementRepository, IOneDayFoodRepository oneDayFoodRepository)
        {
            _oneDayWorkoutRepository = oneDayWorkoutRepository;
            _fillMeasurementRepository = fillMeasurementRepository;
            _oneDayFoodRepository = oneDayFoodRepository;
        }
        // GET: /<controller>/
        public IActionResult WorkoutChart(int id)
        {
            ViewData["Title"] = "Workout statistics";
            ViewData["user"] =id;
            var coll= _oneDayWorkoutRepository.ChartData(id);
           
       return View("Index",coll);
        }
        public IActionResult MeasureChart(int id)
        {
            ViewData["Title"] = "Weight statistics";
            ViewData["user"] = id;
            var coll = _fillMeasurementRepository.ChartItems(id);
   return View("Index", coll.listChart);
        }
        public IActionResult MealChart(int id)
        {
            ViewData["Title"] = "Meals statistics";
            ViewData["user"] = id;
            var coll = _oneDayFoodRepository.ChartItems(id);
            return View("Index", coll);
        }


     
    }
}
