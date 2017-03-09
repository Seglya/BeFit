using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.TagViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Controllers
{
    public class AdminMuscleController: Controller
    {
        private readonly IMusclesRepository _muscleRepository;



        public AdminMuscleController(IMusclesRepository muscleRepository)
        {

            _muscleRepository = muscleRepository;
        }

        // GET: Tag
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
            var collTag = _muscleRepository.MuscleByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collTag = collTag.OrderByDescending(s => s.Name);
                    break;
                default: collTag = collTag.OrderBy(s => s.Name); break;
            }
            int pageSize = 10;
            return View(PagerList<Muscle>.Create(collTag, page ?? 1, pageSize));
        }


        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            return View("Edit");
        }
        // GET: Tag/Create/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var tag = await _muscleRepository.GetMuscleAsync((int)id);
            if (tag == null)
                return View();

            var viewModel = new TagViewModel
            {
                Name = tag.Name,
            };
            ViewData["Title"] = "Edit";
            return View(viewModel);

        }

        // POST: Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagViewModel viewModel, int? id)
        {
            int Id;
            if (id == null)
                Id = 0;
            else
            {
                Id = (int)id;
            }
            try
            {
                if (ModelState.IsValid)
                {



                    var saving1 = await _muscleRepository.SaveMuscleAsync(viewModel.Name, Id);
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

            var muscle = await _muscleRepository.GetMuscleAsync((int)id);
            if (muscle == null)
                return NotFound();
            if (muscle.GroupsOfMuscles.Count != 0)
                return View("ErrorDelete");

            return View(new TagViewModel {Name = muscle.Name});
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _muscleRepository.DeleteMuscle(id);
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

