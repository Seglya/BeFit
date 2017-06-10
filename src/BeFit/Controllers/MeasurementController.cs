using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models.UserProfileViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{ /*[Authorize]*/

    public class MeasurementController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IFillMeasurementRepository _fillMeasurementRepository;
        private readonly IMeasurementRepository _measurementRepository;

        public MeasurementController(IFillMeasurementRepository fillMeasurementRepository,
            IMeasurementRepository measurementRepository, IAppUserRepository appUserRepository)
        {
            _fillMeasurementRepository = fillMeasurementRepository;
            _measurementRepository = measurementRepository;
            _appUserRepository = appUserRepository;
        }

        // GET: /<controller>/
        public IActionResult AddMeasurement(int id)
        {
            var list = new List<AddMeasurementViewModel>();
            foreach (var measurement in _measurementRepository.Measurement)
                list.Add(new AddMeasurementViewModel {Date = DateTime.Today.Date, Measurement = measurement});
            return View(list);
        }

        //POST:/Measurement/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeasurement(int id, List<AddMeasurementViewModel> list)
        {
            if (id == 0)
                id = list.First().AppUserID;
            if (id == 0)
                return RedirectToAction("login", "Account");
            var date = list.First().Date;
            var dates = _fillMeasurementRepository.MeasurementsOnADate(id,date);
            if (dates.Any())
            {
                ModelState.AddModelError("",errorMessage:"Measurement on this date already exist!");
            }
            if(ModelState.IsValid)
            { foreach (var viewModel in list)
            {
                viewModel.Date = date;
                viewModel.AppUserID = id;
                await _fillMeasurementRepository.CreateOrEditFillMeasurementAsync(viewModel.ID, viewModel);
            }
            return RedirectToAction("Detail", new {id=id});}
            list = new List<AddMeasurementViewModel>();
            foreach (var measurement in _measurementRepository.Measurement)
                list.Add(new AddMeasurementViewModel { Date = DateTime.Today.Date, Measurement = measurement });
            return View(list);
        }

        //GET://Measurement/Edit/id
        public IActionResult Edit(int id, string date)
        {
           
            var newDate = DateTime.Parse(date);
            var list = new List<AddMeasurementViewModel>();
            var collMeasure = _fillMeasurementRepository.MeasurementsOnADate(id, newDate);
            foreach (var measurement in collMeasure)
            {
                var viewModel = new AddMeasurementViewModel
                {
                    MeasurementID = measurement.MeasurementID,
                    AppUserID = id,
                    Date = measurement.Date,
                    Measurement = measurement.Measurement,
                    PutMesurement = measurement.PutMesurement,
                    ID = measurement.FillMeasurementID
                };
                list.Add(viewModel);
            }
            return View("AddMeasurement", list);
        }


        public async Task<IActionResult> Detail(int id, string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var list = new List<DetailMeasureVeiwModel>();
            var user = await _appUserRepository.GetUserProfileById(id);
            ViewData["MesuarementAll"] = _measurementRepository.Measurement;
            ViewData["userId"] = id;


            var prevDate = DateTime.MinValue.Date;
            foreach (var measurement in user.Measurements)
            {var detail = new DetailMeasureVeiwModel();
                var date = measurement.Date.Date;
                if (date != prevDate)
                {
                    detail.Date = date;
                    detail.Measure = new List<Measure>();
                    list.Add(detail);
                }
                prevDate = date;
            }
            foreach (var det in list)
            foreach (var measurement in user.Measurements)
            {
                var addMeasure = new Measure();

                if (measurement.Date.Date == det.Date.Date)
                {
                    addMeasure.idMeasure = measurement.MeasurementID;
                    addMeasure.digit = measurement.PutMesurement;
                    det.Measure.Add(addMeasure);
                }
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(s => s.Date).ToList();
                    break;
                default:
                    list = list.OrderBy(s => s.Date).ToList();
                    break;
            }
            return View(list);
        }

        // GET: Measurement/Delete/5
        public IActionResult Delete( string date, int id)
        {
            DateTime dat = DateTime.Parse(date);
            var day = dat.Month;
            var month = dat.Day;
            var year = dat.Year;
           // DateTime newDate = DateTime.Parse(day + "/" + month + "/" + year+" "+"00:00:00");
            var list = new List<AddMeasurementViewModel>();
            var collMeasure = _fillMeasurementRepository.MeasurementsOnADate(id, dat).ToList();
            if (collMeasure.Count > 0)
            {
                foreach (var measurement in collMeasure)
                {
                    var viewModel = new AddMeasurementViewModel
                    {
                        MeasurementID = measurement.MeasurementID,
                        AppUserID = id,
                        Date = measurement.Date,
                        Measurement = measurement.Measurement,
                        PutMesurement = measurement.PutMesurement,
                        ID = measurement.FillMeasurementID
                    };
                    list.Add(viewModel);
                }
                return View("Delete", list);
            }

            return NotFound();
        }

        // POST: Measurement/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(List<AddMeasurementViewModel> list)

        {
            if (list.Count < 1)
                return NotFound();
            var usId = list.First().AppUserID;
            try
            {
                _fillMeasurementRepository.DeleteFillMeasurement(list);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Delete failed. Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return RedirectToAction("Detail", new {id = usId});
        }
    }
}