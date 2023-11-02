using TodaysMonet.Models;
using System.Linq;

namespace TodaysMonet.DAL
{
    public class StatusRepository: IStatusRepository
    {
        private readonly StatusContext _context;
        public StatusRepository(StatusContext context)
        {
            this._context = context;
        }
        public IEnumerable<Status> GetMonthlyStatuses()
        {
            return _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >=DateTime.Today.AddMonths(-1)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToList();
        }

        public IEnumerable<Status> GetWeeklyStatuses()
        {
            return _context.Statuses.Where(status => status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)).OrderBy(status => status.Timestamp).ThenBy(status => status.StatusType).ToList();
        }

        public IEnumerable<Status> GetMonthlyStatusesByType(Statuses StatusType) 
        {
            return _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddMonths(-1)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToList();
        }

        public IEnumerable<Status> GetWeeklyStatusesByType(Statuses StatusType)
        {
            return _context.Statuses.Where(status => (status.Timestamp <= DateTime.UtcNow && status.Timestamp >= DateTime.UtcNow.AddDays(-7)) && status.StatusType == StatusType).OrderBy(status => status.Timestamp).ToList();
        }

        public Status GetDailyStatus(Statuses StatusType)
        {
            return;
        }
    }
}
