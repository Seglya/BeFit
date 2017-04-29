using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class NewsController : Controller
    {
        private INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        // GET: /<controller>/
        public ActionResult List(string sortOrder, string filter, string pageSize, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!string.IsNullOrEmpty(pageSize))
                ViewData["SizePage"] = pageSize;
            else if (string.IsNullOrEmpty(pageSize) & string.IsNullOrEmpty((string)ViewData["SizePage"]))
                pageSize = (string)ViewData["SizePage"];


            int sizeOfPage;
            if (int.TryParse(pageSize, out sizeOfPage) == false)
                sizeOfPage = 6;
            if (filter != null)
                page = 1;
            else
                filter = currentFilter;
            ViewData["CurrentFilter"] = filter;
            var collnews = _newsRepository.NewsByFilter(filter);

            switch (sortOrder)
            {
                case "name_desc":
                    collnews = collnews.OrderByDescending(s => s.Name);
                    break;
                default:
                    collnews = collnews.OrderBy(s => s.Name);
                    break;
            }

            return View(PagerList<News>.Create(collnews, page ?? 1, sizeOfPage));
        }
    }
}
