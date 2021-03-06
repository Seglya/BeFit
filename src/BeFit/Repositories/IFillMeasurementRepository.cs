﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Repositories
{
    public interface IFillMeasurementRepository
    {
        IEnumerable<FillMeasurement> MeasurementsByUserId(int userId);
        IEnumerable<FillMeasurement> MeasurementsOnADate(int userId, DateTime date);
        Task<FillMeasurement> CreateOrEditFillMeasurementAsync(int id, AddMeasurementViewModel viewModel);
        void DeleteFillMeasurement(List<AddMeasurementViewModel> list);
        Task<FillMeasurement> MeasurementsByIdAsync(int Id);
        Chart ChartItems(int id);
    }

    public class FillMeasurementRepository : IFillMeasurementRepository
    {
        private readonly BeFitDbContext _context;

        public FillMeasurementRepository(BeFitDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FillMeasurement> MeasurementsByUserId(int userId)
        {
            if (userId != 0)
                return _context.FillMeasurement.AsNoTracking().Where(s => s.AppUserID == userId);
            return null;
        }

        public async Task<FillMeasurement> MeasurementsByIdAsync(int Id)
        {
            if (Id != 0)
            {
                var ret =
                    await _context.FillMeasurement.AsNoTracking().SingleOrDefaultAsync(i => i.FillMeasurementID == Id);
                return ret;
            }
            return null;
        }


        public IEnumerable<FillMeasurement> MeasurementsOnADate(int userId, DateTime date)
        {
            if (userId != 0)
                return
                    _context.FillMeasurement.Include(m => m.Measurement)
                        .Where(s => (s.AppUserID == userId) & (s.Date == date));
            return null;
        }

        public async Task<FillMeasurement> CreateOrEditFillMeasurementAsync(int id, AddMeasurementViewModel viewModel)
        {
            var fill = new FillMeasurement();
            if (id != 0)
                fill = await _context.FillMeasurement.FindAsync(id);

            fill.PutMesurement = viewModel.PutMesurement;
            if (id == 0)
            {
                fill.AppUserID = viewModel.AppUserID;
                fill.Date = viewModel.Date;
                fill.MeasurementID = viewModel.MeasurementID;
                await _context.FillMeasurement.AddAsync(fill);
                await _context.SaveChangesAsync();
                return await _context.FillMeasurement.LastAsync();
            }
            await _context.SaveChangesAsync();
            return fill;
        }

        public void DeleteFillMeasurement(List<AddMeasurementViewModel> list)
        {
            var delList = new List<FillMeasurement>();
            foreach (var viewModel in list)
                delList.Add(_context.FillMeasurement.Find(viewModel.ID));
            if (delList.Any())
            {
                _context.FillMeasurement.RemoveRange(delList);
                _context.SaveChanges();
            }
        }
        public Chart ChartItems(int id)
        {
var ifNull =(double) _context.AppUser.Find(id).CurrentWeight;
            var coll = _context.FillMeasurement.OrderBy(t => t.Date).Include(t => t.Measurement).Where(t => t.AppUserID == id);
            var start = _context.AppUser.Find(id).DateOfRegoistration;
            var max = (DateTime.Today - start).Days;
            var cartItems = new List<chartItem>();
            for (var i = 0; i <= max; i++)
            {
                
                foreach (var measure in coll)
                {
                    if(measure.Date.Date == start.AddDays(i).Date & measure.Measurement.Name=="Weight")
                    {
                        ifNull = measure.PutMesurement;
                        cartItems.Add( 
                         new chartItem
                        {
                            date = start.AddDays(i),
                            visits = measure.PutMesurement
                        });}
                    else
                    {
                        cartItems.Add(new chartItem { date = start.AddDays(i), visits = ifNull });
                    }
                        
                }

            }
            return new Chart {listChart = cartItems};
        }
    }
}