using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("Monthly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatuses()
        {
            var result = await _statusRepository.GetMonthlyStatuses();
            return result;
        }

        [HttpGet("Weekly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatuses()
        {
            var result = await _statusRepository.GetWeeklyStatuses();
            return result;
        }

        [HttpGet("Monthly/{StatusType:Status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatusesByType(Statuses StatusType)
        {
            var result = await _statusRepository.GetMonthlyStatusesByType(StatusType);
            return result;
        }
    }
}
