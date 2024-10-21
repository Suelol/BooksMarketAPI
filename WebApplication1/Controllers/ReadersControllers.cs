using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : Controller
    {
        private readonly IReadersService _readersService;

        public ReadersController(IReadersService readersService)
        {
            _readersService = readersService; ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Readers>>> GetAllReaders()
        {
            var readers = await _readersService.GetAllReadersAsync();
            return Ok(readers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Readers>> GetReader(int id)
        {
            var reader = await _readersService.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return Ok(reader);
        }

        [HttpPost]
        public async Task<ActionResult<Readers>> CreateReader(Readers reader)
        {
            var createdReader = await _readersService.AddReaderAsync(reader);
            return CreatedAtAction(nameof(GetReader), new { id = createdReader.Id_Reader }, createdReader);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReader(int id, Readers reader)
        {
            var updatedReader = await _readersService.UpdateReaderAsync(id, reader);
            if (updatedReader == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(int id)
        {
            var result = await _readersService.DeleteReaderAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Readers>>> SearchReaders([FromQuery] string searchTerm)
        {
            var readers = await _readersService.SearchReadersAsync(searchTerm);
            return Ok(readers);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<Readers>>> GetReadersPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (readers, totalCount) = await _readersService.GetReadersPaginatedAsync(page, pageSize);
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            return Ok(readers);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalReadersCount()
        {
            var count = await _readersService.GetTotalReadersCountAsync();
            return Ok(count);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportReaders(IEnumerable<Readers> readers)
        {
            await _readersService.ImportReadersAsync(readers);
            return Ok();
        }

        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<Readers>>> ExportReaders()
        {
            var readers = await _readersService.ExportReadersAsync();
            return Ok(readers);
        }
    }

}
