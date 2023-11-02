using TodaysMonet.Models;

namespace TodaysMonet.DAL
{
    public interface IStatusRepository: IDisposable
    {
        IEnumerable<Status> GetMonthlyStatuses();
        IEnumerable<Status> GetWeeklyStatuses();
        IEnumerable<Status> GetMonthlyStatusesByType(Statuses StatusType);
        IEnumerable<Status> GetWeeklyStatusesByType(Statuses StatusType);
        Status GetDailyStatus(Statuses StatusType);
        void InsertDailyStatus(Status status);
        void UpdateDailyStatus(Status status);
        void DeleteDailyStatus(Status status);
        void Save();

    }
}











