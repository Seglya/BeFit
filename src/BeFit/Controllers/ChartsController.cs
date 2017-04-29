using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Razor.Parser.SyntaxConstants;

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
            var coll= _oneDayWorkoutRepository.ChartData(id);
           
       return View("Index",coll);
        }
        public IActionResult MeasureChart(int id)
        {
            var coll = _fillMeasurementRepository.ChartItems(id);
   return View("Index", coll.listChart);
        }
        public IActionResult MealChart(int id)
        {
            var coll = _oneDayFoodRepository.ChartItems(id);
            return View("Index", coll);
        }


     
    }
}
