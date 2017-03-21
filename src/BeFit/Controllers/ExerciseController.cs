using System.Linq;
using BeFit.Models;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IGroupOfMusclesRepository _groupOfMusclesRepository;
        private readonly IMusclesRepository _muscleRepository;
        private readonly IExerciseRepository _repository;

        public ExerciseController(IGroupOfMusclesRepository groupOfMusclesRepository, IExerciseRepository repository,
            IMusclesRepository musclesRepository)
        {
            _groupOfMusclesRepository = groupOfMusclesRepository;
            _repository = repository;
            _muscleRepository = musclesRepository;
        }

        public ActionResult List(string sortOrder, string filter, string pageSize, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!string.IsNullOrEmpty(pageSize))
                ViewData["SizePage"] = pageSize;
            else if (string.IsNullOrEmpty(pageSize) & string.IsNullOrEmpty((string) ViewData["SizePage"]))
                pageSize = (string) ViewData["SizePage"];


            int sizeOfPage;
            if (int.TryParse(pageSize, out sizeOfPage) == false)
                sizeOfPage = 6;
            if (filter != null)
                page = 1;
            else
                filter = currentFilter;
            ViewData["CurrentFilter"] = filter;
            var collExercises = _repository.ExercisesByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collExercises = collExercises.OrderByDescending(s => s.Name);
                    break;
                default:
                    collExercises = collExercises.OrderBy(s => s.Name);
                    break;
            }

            return View(PagerList<Exercise>.Create(collExercises, page ?? 1, sizeOfPage));
        }
    }
}