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
    }

    public class TagRepository:ITagRepository
    {
        private BeFitDbContext _context;

        public TagRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> Tags => _context.Tag.AsNoTracking();
    }
}
