using TodaysMonet.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TodaysMonet.DAL
{
    public class StatusRepository : IStatusRepository
    {
        private readonly StatusContext _context;
        public StatusRepository(StatusContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Status>> GetMonthlyStatuses()
        {
            return await _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.Today.AddMonths(-1)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToListAsync();
        }

        public async Task<IEnumerable<Status>> GetWeeklyStatuses()
        {
            return await _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToListAsync();
        }

        public async Task<IEnumerable<Status>> GetMonthlyStatusesByType(Statuses StatusType)
        {
            return await _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddMonths(-1)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToListAsync();
        }

        public IEnumerable<Status> GetWeeklyStatusesByType(Statuses StatusType)
        {
            return _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToList();
        }

        public Status GetDailyStatus(Statuses StatusType)
        {
            var status = _context.Statuses.Where(status => status.StatusType == StatusType && status.Timestamp.Date == DateTime.Now.Date).FirstOrDefault();
            return status;
        }

        public void InsertDailyStatus(Status Status)
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
