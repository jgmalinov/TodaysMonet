﻿using TodaysMonet.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TodaysMonet.DAL
{
    public class StatusRepository : IStatusRepository
    {
        private readonly StatusContext _context;
        public StatusRepository(StatusContext context)
        {
            this._context = context;
        }
        public async Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatuses()
        {
            var result = await _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.Today.AddMonths(-1)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToListAsync();
            return result;
        }

        public async Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatuses()
        {
            return await _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatusesByType(Statuses StatusType)
        {
            return await _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddMonths(-1)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatusesByType(Statuses StatusType)
        {
            return await _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToListAsync();
        }

        public async Task<ActionResult<Status>> GetDailyStatusByType(Statuses StatusType)
        {
            var status = await _context.Statuses.Where(status => status.StatusType == StatusType && status.Timestamp.Date == DateTime.Now.Date).FirstOrDefaultAsync();
            return status;
        }

        public Task<ActionResult<Status>> PostDailyStatus(Status Status)
        {
            _context.Statuses.Add(Status);
            _context.SaveChanges();
        }

        public void UpdateDailyStatus(Status Status)
        {
            _context.Statuses.Update(Status);
            _context.SaveChanges();
        }

        public void DeleteDailyStatus(Status Status)
        {
            _context.Statuses.Remove(Status);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();    
        }
    }
}
