using Microsoft.AspNetCore.Mvc;
using TodaysMonet.Models;

namespace TodaysMonet.DAL
{
    public interface IStatusRepository: IDisposable
    {
        Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatuses();
        Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatuses();
        Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatusesByType(Statuses StatusType);
        Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatusesByType(Statuses StatusType);
        Task<ActionResult<Status>> GetDailyStatusByType(Statuses StatusType);
        Task PostDailyStatus(Status status);
        Task UpdateDailyStatus(Status status);
        Task DeleteDailyStatus(Status status);
        bool StatusItemExists(Status status);
    }
}











