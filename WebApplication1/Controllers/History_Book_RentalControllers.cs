using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class History_Book_RentalControllers : Controller
    {
        private readonly IHistoryBookRentalService _historyService;

        public History_Book_RentalControllers(IHistoryBookRentalService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<History_Book_Rental>>> GetAllHistory()
        {
            var history = await _historyService.GetAllHistoryAsync();
            return Ok(history);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History_Book_Rental>> GetHistory(int id)
        {
            var history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<History_Book_Rental>> CreateHistory(History_Book_Rental history)
        {
            var createdHistory = await _historyService.AddHistoryAsync(history);
            return CreatedAtAction(nameof(GetHistory), new { id = createdHistory.Id }, createdHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistory(int id, History_Book_Rental history)
        {
            var updatedHistory = await _historyService.UpdateHistoryAsync(id, history);
            if (updatedHistory == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            var result = await _historyService.DeleteHistoryAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<History_Book_Rental>>> SearchHistory([FromQuery] string searchTerm)
        {
            var history = await _historyService.SearchHistoryAsync(searchTerm);
            return Ok(history);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<History_Book_Rental>>> GetHistoryPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (histories, totalCount) = await _historyService.GetHistoryPaginatedAsync(page, pageSize);
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            return Ok(histories);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalHistoryCount()
        {
            var count = await _historyService.GetTotalHistoryCountAsync();
            return Ok(count);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportHistory(IEnumerable<History_Book_Rental> histories)
        {
            await _historyService.ImportHistoryAsync(histories);
            return Ok();
        }

        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<History_Book_Rental>>> ExportHistory()
        {
            var histories = await _historyService.ExportHistoryAsync();
            return Ok(histories);
        }
    }

}
