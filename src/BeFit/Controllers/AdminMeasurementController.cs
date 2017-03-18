using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.MeasurementViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class AdminMeasurementController : Controller
    {
        private IMeasurementRepository _measurementRepository;

        public AdminMeasurementController(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }
        // GET: /Measurement/
        public IActionResult Index(string sortOrder, string pageSize, string filter, string currentFilter, int? page)
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
            }
            ViewData["CurrentFilter"] = filter;
            var collTag = _measurementRepository.MeasurementByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collTag = collTag.OrderByDescending(s => s.Name);
                    break;
               default: collTag = collTag.OrderBy(s => s.Name); break;
            }

            return View(PagerList<Measurement>.Create(collTag, page ?? 1, sizeOfPage));
        }
        // GET: Measurement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return View();
            var measurement = await _measurementRepository.GetMeasurementAsync((int)id);
            if (measurement == null)
                return View();

            var viewModel = new MeasurementViewModel
            {

                Name = measurement.Name,
             UnitsOfMeasurement = measurement.UnitsOfMeasurement
            };

            return View(viewModel);

        }

        // POST: Measurement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MeasurementViewModel viewModel)
        {

            if (id == null)
                id = 0;
            var measure = await _measurementRepository.GetMeasurementByNameAsync(viewModel.Name);
            
            if (measure!=default(Measurement)  )
               {
                if (id == 0)
                    ModelState.AddModelError("", "Measurement with this name already exist!!!");
            
            if (measure.MeasurementID!=id & id != 0)
               ModelState.AddModelError("","Measurement with this name already exist!!!");}
            try
            {
                if (ModelState.IsValid)
                {

                    var saving1 = await _measurementRepository.SaveMeasurementAsync(viewModel, (int)id);
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

            var Measurement = await _measurementRepository.GetMeasurementAsync((int)id);
            if (Measurement == null)
                return NotFound();

            return View(new MeasurementViewModel { Name = Measurement.Name, UnitsOfMeasurement = Measurement.UnitsOfMeasurement });
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _measurementRepository.DeleteMeasurement(id);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return RedirectToAction("Index");
        }
        // GET: /<controller>/
    
    }
}
