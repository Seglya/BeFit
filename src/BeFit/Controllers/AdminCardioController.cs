using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.CardioViewModels;
using BeFit.Models.FoodViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class AdminCardioController : Controller
    {
        private ICardioRepository _cardioRepository;

        public AdminCardioController(ICardioRepository cardioRepository)
        {
            _cardioRepository = cardioRepository;
        }
        
        // GET: /Cardio/
        public IActionResult Index(string sortOrder, string pageSize, string filter, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CalSortParam"] = string.IsNullOrEmpty(sortOrder) ? "cal_desc" : "cal";

            if (!string.IsNullOrEmpty(pageSize))
                ViewData["SizePage"] = pageSize;
            else if (string.IsNullOrEmpty(pageSize) & string.IsNullOrEmpty((string)ViewData["SizePage"]))
                pageSize = (string)ViewData["SizePage"];


            int sizeOfPage;
            if (int.TryParse(pageSize, out sizeOfPage) == false)
                sizeOfPage = 12;
            if (filter != null)
                page = 1;
            else
                filter = currentFilter;
            ViewData["CurrentFilter"] = filter;
            var collTag = _cardioRepository.CardiosByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collTag = collTag.OrderByDescending(s => s.Name);
                    break;
                case "cal_desc":
                    collTag = collTag.OrderByDescending(s => s.CalPerHour);
                    break;
                case "cal":
                    collTag = collTag.OrderBy(s => s.CalPerHour);
                    break;
               
                default:
                    collTag = collTag.OrderBy(s => s.Name);
                    break;
            }

            return View(PagerList<Cardio>.Create(collTag, page ?? 1, sizeOfPage));
        }

        // GET: Cardio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return View();
            var cardio = await _cardioRepository.GetCardioAsync((int)id);
            if (cardio == null)
                return View();

            var viewModel = new CardioViewModel
            {
                Name = cardio.Name,
                CalPerHour = cardio.CalPerHour,
               
            };

            return View(viewModel);
        }

        // POST: Cardio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CardioViewModel viewModel)
        {
            if (id == null)
                id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var saving1 = await _cardioRepository.AddOrEditCardioAsync(viewModel, (int)id);
                    if (saving1 != 0)
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

        // GET: Cardio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cardio = await _cardioRepository.GetCardioAsync((int)id);
            if (cardio == null)
                return NotFound();

            return
                View(new CardioViewModel
                {
                    Name = cardio.Name,
                 CalPerHour = cardio.CalPerHour
                });
        }

        // POST: Cardio/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _cardioRepository.DeleteCardio(id);
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
