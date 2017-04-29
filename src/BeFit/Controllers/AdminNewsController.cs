using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeFit.Models;
using BeFit.Models.FoodViewModels;
using BeFit.Models.NewsViewModels;
using BeFit.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeFit.Controllers
{
    public class AdminNewsController : Controller
    {
        private INewsRepository _newsRepository;

        public AdminNewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        // GET: /<controller>/
        public IActionResult Index(string sortOrder, string pageSize, string filter, string currentFilter, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           
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

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return View(new AddNewsViewModel());
            var news = await _newsRepository.GetNewsAsync((int)id);
            if (news == null)
                return View(new AddNewsViewModel());

            var viewModel = new AddNewsViewModel()
            {
                Name = news.Name,
                Path = news.Path,
                ImagePath = news.ImagePath,
                Tag = news.Tag

            };

            return View(viewModel);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AddNewsViewModel viewModel)
        {
            if (id == null)
                id = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.ITextFile != null)
                    {
                        var Path = @"../BeFit/wwwroot/Files/" + viewModel.ITextFile.FileName;

                        using (var stream = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            await viewModel.ITextFile.CopyToAsync(stream);
                            viewModel.Path = @"/Files/" + viewModel.ITextFile.FileName;
                            
                        }
                    }
                    if (viewModel.IImageFile != null)
                    {
                        var Path = @"../BeFit/wwwroot/Files/" + viewModel.IImageFile.FileName;

                        using (var stream = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            await viewModel.IImageFile.CopyToAsync(stream);
                            viewModel.ImagePath = @"/Files/" + viewModel.IImageFile.FileName;
                            
                        }
                    }
                    var saving1 = await _newsRepository.SaveNewsAsync(viewModel, (int)id);
                    if (saving1 != null)
                        return View("Details", saving1);
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

        public async Task<IActionResult> EditNews(string FileName, string name)
        {
            var news = await _newsRepository.GetNewsByFileNameAsync(FileName,name);
            if (news == null)
                return NotFound();

            return RedirectToAction("Edit", new {id = news.NewsID});
        }
        public async Task<IActionResult> Details(int id=19)
        {
            var news = await _newsRepository.GetNewsAsync(id);
            if (news == null)
                return NotFound();
        string[] text1 = System.IO.File.ReadAllLines(@"../BeFit/wwwroot" + news.Path);
          return View(new NewsViewModel { Article = text1, Name = news.Name, ImagePath = news.ImagePath, Tag = news.Tag });
        }


        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var news = await _newsRepository.GetNewsAsync((int)id);
            if (news == null)
                return NotFound();

            return
                View( new NewsViewModel
                {
                    Name = news.Name,
                  });
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _newsRepository.DeleteNews(id);
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
