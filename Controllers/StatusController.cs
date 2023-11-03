using Microsoft.AspNetCore.Mvc;
using TodaysMonet.Models;
using TodaysMonet.DAL;

namespace TodaysMonet.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StatusController
    {
        private IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository)
        {
            this._statusRepository = statusRepository;
        }
        [HttpGet]
        public Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatuses()
        {
            var result = _statusRepository.GetMonthlyStatuses();
            return result;
        }
    }
}
