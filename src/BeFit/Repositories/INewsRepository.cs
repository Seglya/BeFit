using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.NewsViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
   public interface INewsRepository
    {
        IEnumerable<News> News { get; }
        Task<NewsViewModel> SaveNewsAsync(AddNewsViewModel viewModel, int id);
        IEnumerable<News> NewsByFilter(string filter);
        void DeleteNews(int id);
        Task<News> GetNewsAsync(int id);
        Task<News> GetNewsByFileNameAsync(string FileName, string name);
    }

    public class NewsRepository : INewsRepository
    {
        private readonly BeFitDbContext _context;

        public NewsRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<News> News => _context.News.AsNoTracking();

        public async Task<NewsViewModel> SaveNewsAsync(AddNewsViewModel viewModel, int id = 0)
        {
            if (id == 0)
            {
                _context.News.Add(new News
                {
                    Name = viewModel.Name,
                    Path = viewModel.Path,
                    Tag = viewModel.Tag,
                    ImagePath = viewModel.ImagePath
                });
                _context.SaveChanges();
                var n = await _context.News.LastAsync();
                string text1 = File.ReadAllText(@"../BeFit/wwwroot"+n.Path);
                return new NewsViewModel { Name = n.Name, ImagePath = n.ImagePath, Tag = n.Tag};
            }
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return null;
            }
            news.Name = viewModel.Name;
            news.Path = viewModel.Path;
            news.Tag = viewModel.Tag;
            news.ImagePath = viewModel.ImagePath;
            string text = File.ReadAllText(@"../BeFit/wwwroot"+news.Path);
            return new NewsViewModel { Name = news.Name, ImagePath = news.ImagePath, Tag = news.Tag};
        }

        public IEnumerable<News> NewsByFilter(string filter)
        {
            if (filter != null)
                return _context.News.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return _context.News.AsNoTracking();
        }

        public async Task<News> GetNewsAsync(int id = 0)
        {
            if (id != 0)
                return await _context.News.FindAsync(id);
            return null;
        }

        public void DeleteNews(int id)
        {
            var del = _context.News.Find(id);
            if (del != null)
            {
                _context.News.Remove(del);
                _context.SaveChanges();
            }
        }
        public async Task<News> GetNewsByFileNameAsync(string FileName, string name)
        {
            var news = await _context.News.Where(t => t.ImagePath == FileName).SingleOrDefaultAsync(t => t.Name == name);
            return news;
        }
    }
}
