using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;

using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
  public  interface ITagRepository
    {
        IEnumerable<Tag> Tags { get; }
        Task<Tag> SaveTagAsync( string name,int id);
        IEnumerable<Tag> TagByFilter(string filter);
        Task<Tag> GetTagAsync(int id);
       void  DeleteTag(int id);
        
    }

    public class TagRepository:ITagRepository
    {
        private BeFitDbContext _context;

        public TagRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> Tags => _context.Tag.AsNoTracking();

        public async Task<Tag> SaveTagAsync(string name, int id = 0)
        {
            if (id == 0)
            {
                await _context.Tag.AddAsync(new Tag {Name = name});
                await _context.SaveChangesAsync();
                return await _context.Tag.SingleOrDefaultAsync(t => t.Name == name);
            }
             _context.Tag.Update(new Tag {TagID = id, Name = name});
            await _context.SaveChangesAsync();
            return await _context.Tag.FindAsync(id);
        }
        public IEnumerable<Tag> TagByFilter(string filter)
        {
            if (filter != null)
                return _context.Tag.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return _context.Tag.AsNoTracking();
        }

        public async Task<Tag> GetTagAsync(int id=0)
        {if(id!=0)
            return await _context.Tag.Include(s=>s.Workouts).SingleOrDefaultAsync(i=>i.TagID==id);
            else
            {
                return null;
            }
        }

        public void DeleteTag(int id)
        {
            var del = _context.Tag.Find(id);
            if (del != null)
            {
                _context.Tag.Remove(del);
                 _context.SaveChanges();
            }
        }
    }
}
