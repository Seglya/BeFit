using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.CardioViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface ICardioRepository
    {
        IEnumerable<Cardio> Cardios { get; }
        IEnumerable<Cardio> CardiosByFilter(string filter);
        Task<Cardio> GetCardioAsync(int? id);
        Cardio GetCardio(string name);
        bool DeleteCardio(int id);
        Task<int> AddOrEditCardioAsync(CardioViewModel viewModel, int id);

    }

    public class CardioRepository : ICardioRepository
    {
        private readonly BeFitDbContext context;

        public CardioRepository(BeFitDbContext cont)
        {
            context = cont;
        }


        public IEnumerable<Cardio> Cardios => context.Cardio.AsNoTracking();

        public IEnumerable<Cardio> CardiosByFilter(string filter)
        {
            if (filter != null)
                return context.Cardio.Where(s => s.Name.Contains(filter)).AsNoTracking();
            return context.Cardio.AsNoTracking();
        }

        public async Task<Cardio> GetCardioAsync(int? id)
        {
            return
                await context.Cardio.FindAsync(id);
        }

        public Cardio GetCardio(string name)
        {
            foreach (var ex in context.Cardio)
                if (ex.Name == name)
                    return ex;
            return null;
        }

        public bool DeleteCardio(int id)
        {
            var del = context.Cardio.Find(id);
            if (del != null)
            {
                context.Cardio.Remove(del);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<int> AddOrEditCardioAsync(CardioViewModel viewModel, int id = 0)
        {
           var Cardio = await GetCardioAsync(id) ?? new Cardio();

            Cardio.Name = viewModel.Name;
            Cardio.CalPerHour = viewModel.CalPerHour;
            if (id == 0)
            {
                context.Cardio.Add(Cardio);
                context.SaveChanges();

                return GetCardio(Cardio.Name).CardioID;
            }

           context.SaveChanges();
            return id;
        }
    }
}
