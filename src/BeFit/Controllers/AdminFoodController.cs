using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.ExerciseViewModels;
using BeFit.Models.FoodViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class AdminFoodController : Controller
    {
        private IFoodRepository _foodRepository;

        public AdminFoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        // GET: /Food/
        public IActionResult Index(string sortOrder, string pageSize, string filter, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CalSortParam"] = string.IsNullOrEmpty(sortOrder) ? "cal_desc" : "cal";
            ViewData["ProtSortParam"] = string.IsNullOrEmpty(sortOrder) ? "prot_desc" : "prot";
            ViewData["FatSortParam"] = string.IsNullOrEmpty(sortOrder) ? "fat_desc" : "fat";
            ViewData["CarbSortParam"] = string.IsNullOrEmpty(sortOrder) ? "carb_desc" : "carb";
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
            }
            ViewData["CurrentFilter"] = filter;
            var collTag = _foodRepository.FoodByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collTag = collTag.OrderByDescending(s => s.Name);
                    break;
                case "cal_desc":
                    collTag = collTag.OrderByDescending(s => s.Calories);
                    break;
                case "cal":
                    collTag = collTag.OrderBy(s => s.Calories);
                    break;
                case "carb_desc":
                    collTag = collTag.OrderByDescending(s => s.Carbohydrate);
                    break;
                case "carb":
                    collTag = collTag.OrderBy(s => s.Carbohydrate);
                    break;
                case "prot_desc":
                    collTag = collTag.OrderByDescending(s => s.Protein);
                    break;
                case "prot":
                    collTag = collTag.OrderBy(s => s.Protein);
                    break;
                case "fat_desc":
                    collTag = collTag.OrderByDescending(s => s.Fat);
                    break;
                case "fat":
                    collTag = collTag.OrderBy(s => s.Fat);
                    break;
                default: collTag = collTag.OrderBy(s => s.Name); break;
            }
             
            return View(PagerList<Food>.Create(collTag, page ?? 1, sizeOfPage));
        }
        // GET: Food/Edit/5
       public  async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return View();
            var food = await _foodRepository.GetFoodAsync((int)id);
            if (food == null)
                return View();
            
            var viewModel =new FoodViewModel
            {
                
                Name = food.Name,
                Calories = food.Calories,
                Protein = food.Protein,
                Fat = food.Fat,
                Carbohydrate = food.Carbohydrate
            };
           
            return View(viewModel);

        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, FoodViewModel viewModel)
        {

            if (id == null)
               id=0;
         try
            {
                if (ModelState.IsValid)
                {

                    var saving1 = await _foodRepository.SaveFoodAsync(viewModel, (int)id);
                    if (saving1 != null)
                        return RedirectToAction("Index");
                }
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

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var food = await _foodRepository.GetFoodAsync((int)id);
            if (food == null)
                return NotFound();

            return View(new FoodViewModel {Name = food.Name, Protein = food.Protein, Fat = food.Fat, Calories = food.Calories, Carbohydrate = food.Carbohydrate});
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _foodRepository.DeleteFood(id);
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
