using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodaysMonet.Models;
using TodaysMonet.DAL;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TodaysMonet.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
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

        [HttpGet("Monthly/{StatusType:Statuses}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetMonthlyStatusesByType(Statuses StatusType)
        {
            var result = await _statusRepository.GetMonthlyStatusesByType(StatusType);
            return result;
        }

        [HttpGet("Monthly/{StatusType:Statuses}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetWeeklyStatusesByType(Statuses StatusType)
        {
            var result = await _statusRepository.GetWeeklyStatusesByType(StatusType);
            return result;
        }

        [HttpGet("Daily/{StatusType:Statuses}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Status>> GetDailyStatusByType(Statuses StatusType)
        {
            var result = await _statusRepository.GetDailyStatusByType(StatusType);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
 
        public async Task<ActionResult<Status>> PostDailyStatus(Status Status)
        {
            await _statusRepository.PostDailyStatus(Status);
            return CreatedAtAction(nameof(GetDailyStatusByType), new { StatusType = Status.StatusType }, Status);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDailyStatus(int id, Status Status)
        {
            if (id != Status.id)
            {
                return BadRequest();
            }

            try
            {
                 await _statusRepository.UpdateDailyStatus(Status);
            }
            catch (DbUpdateConcurrencyException) when (!_statusRepository.StatusItemExists(Status))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
