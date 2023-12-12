using TodaysMonet.Models;
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

        public async Task<ActionResult<IEnumerable<Status>>> GetDailyStatuses()
        {
            return await _context.Statuses.Where(status => status.Timestamp.Date == DateTime.UtcNow.Date).ToListAsync();
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

        public async Task<Status> GetDailyStatusById(int id)
        {
            var result = await _context.Statuses.FirstOrDefaultAsync(status => status.id == id);
            return result;
        }

        public async Task PostDailyStatus(Status Status)
        {
            
            var StatusAlreadyLogged = _context.Statuses.Where(status => status.StatusType == Status.StatusType && status.Timestamp.Date == Status.Timestamp.Date).FirstOrDefault();
            if (StatusAlreadyLogged == null)
            {
                _context.Statuses.Add(Status);
            }
            else
            {
                StatusAlreadyLogged.Timestamp = Status.Timestamp;
                StatusAlreadyLogged.DeviationFromTarget = Status.DeviationFromTarget;
                StatusAlreadyLogged.MinutesLogged = Status.MinutesLogged;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDailyStatus(Status Status)
        {
            
            _context.Statuses.Update(Status);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDailyStatus(Status Status)
        {
            _context.Statuses.Remove(Status);
            await _context.SaveChangesAsync();
        }

        public bool StatusItemExists(int id)
        {
            return _context.Statuses.Any(status => status.id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
