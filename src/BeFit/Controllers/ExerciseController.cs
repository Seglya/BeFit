using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.ExerciseViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Controllers
{
    public class ExerciseController: Controller
    {
        private readonly IMusclesRepository _muscleRepository;
        private readonly IGroupOfMusclesRepository _groupOfMusclesRepository;
        private readonly IExerciseRepository _repository;

        public ExerciseController(IGroupOfMusclesRepository groupOfMusclesRepository, IExerciseRepository repository,
            IMusclesRepository musclesRepository)
        {
            _groupOfMusclesRepository = groupOfMusclesRepository;
            _repository = repository;
            _muscleRepository = musclesRepository;
        }

        public ActionResult List(string sortOrder, string filter, string currentFilter, int? page)
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
            var collExercises = _repository.ExercisesByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collExercises = collExercises.OrderByDescending(s => s.Name);
                    break;
                default: collExercises = collExercises.OrderBy(s => s.Name); break;
            }
            int pageSize = 6;
            return View(PagerList<Exercise>.Create(collExercises, page ?? 1, pageSize));
        }
    }
}
