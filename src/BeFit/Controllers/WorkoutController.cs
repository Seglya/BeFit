using System.Collections.Generic;
using System.Linq;
using BeFit.Models;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IFillingWorkoutRepository _fillingWorkoutRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IWorkoutRepository _workoutRepository;


        public WorkoutController(IExerciseRepository exerciseRepository,
            IWorkoutRepository workoutRepository, ITagRepository tagRepository,
            IFillingWorkoutRepository fillingWorkoutRepository)
        {
            // _context = context;
            _exerciseRepository = exerciseRepository;

            _workoutRepository = workoutRepository;
            _tagRepository = tagRepository;
            _fillingWorkoutRepository = fillingWorkoutRepository;
        }

        public ActionResult List(string sortOrder, string filterTag, string curentTag, string filter, string pageSize,
            string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (!string.IsNullOrEmpty(pageSize))
                ViewData["SizePage"] = pageSize;
            else if (string.IsNullOrEmpty(pageSize) & string.IsNullOrEmpty((string) ViewData["SizePage"]))
                pageSize = (string) ViewData["SizePage"];

            ViewData["AllTags"] = _tagRepository.Tags.ToList();
            int sizeOfPage;
            if (int.TryParse(pageSize, out sizeOfPage) == false)
                sizeOfPage = 12;
            if (filter != null)
                page = 1;
            else
                filter = currentFilter;
            ViewData["CurrentFilter"] = filter;

            IEnumerable<Workout> collworkout;
            if (filterTag == null)
            {
                filterTag = curentTag;
                collworkout = _workoutRepository.WorkoutsByFilter(filter);
            }
            else
            {
                collworkout = _workoutRepository.WorkoutsByFilter(filter).Where(t => t.Tag.Name == filterTag);
            }
            ViewData["FilterTagParam"] = filterTag;
            switch (sortOrder)
            {
                case "name_desc":
                    collworkout = collworkout.OrderByDescending(s => s.Name);
                    break;
                default:
                    collworkout = collworkout.OrderBy(s => s.Name);
                    break;
            }

            return View(PagerList<Workout>.Create(collworkout, page ?? 1, sizeOfPage));
        }
    }
}