using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres_Books>>> GetAllGenres()
        {
            var genres = await _genresService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genres_Books>> GetGenre(int id)
        {
            var genre = await _genresService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genres_Books>> CreateGenre(Genres_Books genre)
        {
            var createdGenre = await _genresService.AddGenreAsync(genre);
            return CreatedAtAction(nameof(GetGenre), new { id = createdGenre.Id_Genres_Books }, createdGenre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, Genres_Books genre)
        {
            var updatedGenre = await _genresService.UpdateGenreAsync(id, genre);
            if (updatedGenre == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _genresService.DeleteGenreAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Genres_Books>>> SearchGenres([FromQuery] string searchTerm)
        {
            var genres = await _genresService.SearchGenresAsync(searchTerm);
            return Ok(genres);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IEnumerable<Genres_Books>>> GetGenresPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (genres, totalCount) = await _genresService.GetGenresPaginatedAsync(page, pageSize);
            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            return Ok(genres);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalGenresCount()
        {
            var count = await _genresService.GetTotalGenresCountAsync();
            return Ok(count);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportGenres(IEnumerable<Genres_Books> genres)
        {
            await _genresService.ImportGenresAsync(genres);
            return Ok("Genres imported successfully");
        }

        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<Genres_Books>>> ExportGenres()
        {
            var genres = await _genresService.ExportGenresAsync();
            return Ok(genres);
        }

    }

}
