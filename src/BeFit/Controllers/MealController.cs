using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class MealController : Controller
    {
        private IFoodRepository _foodRepository;
        private IIngestionRepository _ingestionRepository;
        private IOneDayFoodRepository _oneDayFoodRepository;
        private IWeightOfFoodRepository _weightOfFoodRepository;

        public MealController(IFoodRepository foodRepository, IIngestionRepository ingestionRepository, IOneDayFoodRepository oneDayFoodRepository, IWeightOfFoodRepository weightOfFoodRepository)
        {
            _foodRepository = foodRepository;
            _ingestionRepository = ingestionRepository;
            _oneDayFoodRepository = oneDayFoodRepository;
            _weightOfFoodRepository = weightOfFoodRepository;
        }
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            ViewData["user"] = id;
            var AllOneDayFoodForUser=_oneDayFoodRepository.OneDayFoodsByUserId(id);
            var AllMealsViewModel=new List<MealDetailViewModel>();
            
            foreach (var oneDayFood in AllOneDayFoodForUser)
            {var list=new List<CountMealPerIngestion>();
                var ingList = new List<IngestionViewModel>();
                foreach (var ingestion in oneDayFood.Ingestions)
                {
                    var weightList = new List<WeightOfFoodViewModel>();
                    foreach (var weightOfFood in ingestion.WeightOfFood)
                    {var newWeight=new WeightOfFoodViewModel {Food = weightOfFood.Food, IngestionId = weightOfFood.IngestionID, Weight = weightOfFood.Weight };
                        weightList.Add(newWeight);
                    }
                    var ingViewModel=new IngestionViewModel {WeightOfFood = weightList, Name = ingestion.Name,OneDayFoodId = oneDayFood.OneDayFoodID};
                    ingList.Add(ingViewModel);
                    list.Add(ingViewModel.GetCountMealPerIngestion);
                }
                var newOneDayViewModel = new OneDayFoodViewModel
                {
                    AppUserID = id,
                    Date = oneDayFood.Date,
                    Ingestions = ingList,
                    Water = oneDayFood.Water
                };
                
                AllMealsViewModel.Add(new MealDetailViewModel {CountMealPerIngestions = list,OneDayFoodViewModel =newOneDayViewModel, OneDayFoodId = oneDayFood.OneDayFoodID});
            }
            return View(AllMealsViewModel);
        }
        //GET:/Meal/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            
                var ingections = IngestionViewModel.OneDayIngestions;
            var viewModel=new OneDayFoodViewModel
            {
                AppUserID = id,
                Date = DateTime.Today.Date,
                Ingestions = ingections
          
            };
            PopulateFoodDropDownList();
            return View(viewModel);
        }
        //POST: /Meal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OneDayFoodViewModel viewModel, string[][] Weight, string[][] food)
        {
            var dates = await _oneDayFoodRepository.OneDayFoodByUserIdForDateAsync(viewModel.AppUserID, viewModel.Date);
            if (dates != null)
            {
                ModelState.AddModelError("", errorMessage: "Measurement on this date already exist!");
            }
            if (ModelState.IsValid)
            {
                viewModel.Ingestions = new List<IngestionViewModel>();
            
            var oneDayFood = await _oneDayFoodRepository.AddOrEditOneDayFoodAsync(viewModel, 0);
            int i = 0;
            foreach (var ingestion in IngestionViewModel.OneDayIngestions)
            {
                ingestion.OneDayFoodId = oneDayFood.OneDayFoodID;
                var ing = _ingestionRepository.CreateIngestionsAsync(ingestion).Result;
                if (food[i][0] != null)
                    for (int j = 0; j < food[i].Length; j++)
                    {
                        var weight = new WeightOfFoodViewModel
                        {
                            Food = _foodRepository.GetFoodAsync(int.Parse(food[i][j])).Result,
                            IngestionId = ing.IngestionID,
                            Weight = int.Parse(Weight[i][j])
                        };
                        var weightOffood =
                            _weightOfFoodRepository.CreatetWeightOfFoodAsync(weight).Result;

                    }

                i++;
            }
            return RedirectToAction("Detail", new {id = oneDayFood.OneDayFoodID});
        }
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var DetailViewModel=new MealDetailViewModel { OneDayFoodId = id};
            var oneDayFood = await _oneDayFoodRepository.GetOneDayFoodBiIdAsync(id);
            DetailViewModel.OneDayFoodViewModel=new OneDayFoodViewModel
            {
                AppUserID = oneDayFood.AppUserID,
                Date = oneDayFood.Date,
                Water = oneDayFood.Water
            };
            var collIngestions=new List<IngestionViewModel>();
            foreach (var ingestion in oneDayFood.Ingestions)
            {
                var coll = new List<WeightOfFoodViewModel>();
                foreach (var weightOfFood in ingestion.WeightOfFood)
                {
                    coll.Add(new WeightOfFoodViewModel {Food = weightOfFood.Food, IngestionId = weightOfFood.IngestionID, Weight = weightOfFood.Weight});
                }
                if (coll.Count == 0)
                {
                    coll.Add(new WeightOfFoodViewModel {IngestionId = ingestion.IngestionID});
                }
                collIngestions.Add(new IngestionViewModel
                {
                    Name = ingestion.Name,
                    OneDayFoodId = ingestion.OneDayFoodID,
                    WeightOfFood = coll,
                });
            }
            DetailViewModel.OneDayFoodViewModel.Ingestions = collIngestions;
            DetailViewModel.CountMealPerIngestions=new List<CountMealPerIngestion>();
            foreach (var ingestionViewModel in collIngestions)
            {
                DetailViewModel.CountMealPerIngestions.Add(ingestionViewModel.GetCountMealPerIngestion);
            }
            
            
            return View(DetailViewModel);

        }
        //GET:/Meal/Edit/ingestionId
        public IActionResult Edit(int id)
        {var ing=_ingestionRepository.GetIngestionByIdAsync(id).Result;
            var weightOfFood = new List<WeightOfFoodViewModel>();
    
                if (ing.WeightOfFood!=null)
           foreach (var item in ing.WeightOfFood)
            {
               
                var food = _foodRepository.GetFoodAsync(item.FoodID).Result;
                weightOfFood.Add(new WeightOfFoodViewModel {Food = food,FoodID = food.FoodID, IngestionId = item.IngestionID, Weight = item.Weight});
            }
            if (weightOfFood.Count == 0)
            {
                weightOfFood.Add(new WeightOfFoodViewModel { IngestionId = id });
            }
            var ingestionViewModel = new IngestionViewModel
            {
                Name = ing.Name,
                OneDayFoodId=ing.OneDayFoodID,
                WeightOfFood = weightOfFood,
               
            };
            PopulateFoodDropDownList();
            return View(ingestionViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string[][] Weight, string[][] food)
        {
            _weightOfFoodRepository.DeleteWeightOfFoodByIngestId(id);
            var weghtOfFoodViewMod=new WeightOfFoodViewModel();
            for (int i=0;i<Weight[0].Length;i++)
            {
                weghtOfFoodViewMod.Weight =double.Parse(Weight[0][i]);
                weghtOfFoodViewMod.Food = _foodRepository.GetFoodAsync(int.Parse(food[0][i])).Result;
                weghtOfFoodViewMod.FoodID = int.Parse(food[0][i]);
                weghtOfFoodViewMod.IngestionId = id;
                var newWeight = _weightOfFoodRepository.CreatetWeightOfFoodAsync(weghtOfFoodViewMod);

            }
            return RedirectToAction("Detail", new {id = _ingestionRepository.GetIngestionByIdAsync(id).Result.OneDayFoodID});
        }

        public IActionResult DeleteIngestion(int id)
        {
           
            _weightOfFoodRepository.DeleteWeightOfFoodByIngestId(id);
            var ing=_ingestionRepository.GetIngestionByIdAsync(id);
            return RedirectToAction("Detail", new {id = ing.Result.OneDayFoodID});
           
        }
        //GET:/Meal/Delete/id
        public IActionResult Delete(int id)
        {var del=_oneDayFoodRepository.GetOneDayFoodBiIdAsync(id).Result;
            ViewData["Date"] = del.Date;
            ViewData["oneDayId"] = id;
            ViewData["user"] = del.AppUserID;
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id, int user)
        {
            _oneDayFoodRepository.DeleteOneDayFood(id);
            return RedirectToAction("Index", new {id=user});
        }
        private void PopulateFoodDropDownList(object selectedFood = null)
        {
            var food = _foodRepository.Food;
            ViewBag.FoodID = new SelectList(food, "FoodID", "Name", selectedFood);
        }
    }
}
