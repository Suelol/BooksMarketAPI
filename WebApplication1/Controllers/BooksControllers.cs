using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _booksService.GetAllBooksAsync();
            return Ok(new { books, status = true });
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found", status = false });
            }
            return Ok(new { book, status = true });
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Books newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data", status = false });
            }

            await _booksService.AddBookAsync(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id_Books }, new { book = newBook, status = true });
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Books updatedBook)
        {
            if (id != updatedBook.Id_Books)
            {
                return BadRequest(new { message = "ID mismatch", status = false });
            }

            await _booksService.UpdateBookAsync(id, updatedBook);
            return Ok(new { message = "Book updated successfully", status = true });
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _booksService.DeleteBookAsync(id);
            return Ok(new { message = "Book deleted successfully", status = true });
        }

        // GET: api/books/genre/{genreId}
        [HttpGet("genre/{genreId}")]
        public async Task<IActionResult> GetBooksByGenre(int genreId)
        {
            var books = await _booksService.GetBooksByGenreAsync(genreId);
            return Ok(new { books, status = true });
        }

        // GET: api/books/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string author, [FromQuery] string genre, [FromQuery] int? year)
        {
            var books = await _booksService.SearchBooksAsync(author, genre, year);
            return Ok(new { books, status = true });
        }

        // GET: api/books/list
        [HttpGet("list")]
        public async Task<IActionResult> GetBookList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _booksService.GetBooksPaginatedAsync(page, pageSize);
            var totalBooks = await _booksService.GetTotalBooksCountAsync();
            return Ok(new
            {
                books,
                currentPage = page,
                totalItems = totalBooks,
                totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize),
                status = true
            });
        }

        // POST: api/books/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportBooks(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "File is empty", status = false });

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var books = csv.GetRecords<Books>().ToList();
                    await _booksService.ImportBooksAsync(books);
                }
                return Ok(new { message = "Books imported successfully", status = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred during import: {ex.Message}", status = false });
            }
        }

        // GET: api/books/export
        [HttpGet("export")]
        public async Task<IActionResult> ExportBooks()
        {
            try
            {
                var books = await _booksService.ExportBooksAsync();

                using (var memoryStream = new MemoryStream())
                using (var writer = new StreamWriter(memoryStream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(books);
                    writer.Flush();
                    return File(memoryStream.ToArray(), "text/csv", "books.csv");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred during export: {ex.Message}", status = false });
            }
        }


    }

}
