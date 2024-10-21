using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

public class BooksService : IBooksService  // Реализуем интерфейс IBooksService
{
    private readonly TestApiDb _context;

    public BooksService(TestApiDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Books>> GetAllBooksAsync()
    {
        return await _context.Books.Include(b => b.Genres).ToListAsync();
    }

    public async Task<Books> GetBookByIdAsync(int id)
    {
        return await _context.Books.Include(b => b.Genres).FirstOrDefaultAsync(b => b.Id_Books == id);
    }

    public async Task AddBookAsync(Books newBook)
    {
        await _context.Books.AddAsync(newBook);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(int id, Books updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre_ID = updatedBook.Genre_ID;
            book.PublicationYear = updatedBook.PublicationYear;
            book.Description = updatedBook.Description;
            book.AvailableCopies = updatedBook.AvailableCopies;
            book.Year = updatedBook.Year;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Books>> GetBooksByGenreAsync(int genreId)
    {
        return await _context.Books
            .Where(b => b.Genre_ID == genreId.ToString())
            .Include(b => b.Genres)
            .ToListAsync();
    }

    public async Task<IEnumerable<Books>> SearchBooksAsync(string query)
    {
        return await _context.Books
            .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
            .Include(b => b.Genres)
            .ToListAsync();
    }

    public async Task<IEnumerable<Books>> SearchBooksAsync(string author, string genre, int? year)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genre_ID.Contains(genre));
        }

        //if (year.HasValue)
        //{
        //    query = query.Where(b => b.Year.HasValue && b.Year.Value.Year == year.Value);
        //}

        return await query.Include(b => b.Genres).ToListAsync();
    }

    public async Task<IEnumerable<Books>> GetBooksPaginatedAsync(int page, int pageSize)
    {
        return await _context.Books
            .Include(b => b.Genres)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task ImportBooksAsync(IEnumerable<Books> books)
    {
        await _context.Books.AddRangeAsync(books);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Books>> ExportBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Genres)
            .ToListAsync();
    }

    // Дополнительные методы, которые могут быть полезны

    public async Task<int> GetTotalBooksCountAsync()
    {
        return await _context.Books.CountAsync();
    }


    public async Task<IEnumerable<Books>> GetRecentlyAddedBooksAsync(int count)
    {
        return await _context.Books
            .Include(b => b.Genres)
            .OrderByDescending(b => b.DateAdded)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Books>> GetMostPopularBooksAsync(int count)
    {
        return await _context.Books
            .Include(b => b.Genres)
            .OrderByDescending(b => _context.History_Book_Rentals.Count(r => r.Books_Id == b.Id_Books))
            .Take(count)
            .ToListAsync();
    }

    public async Task<bool> IsBookAvailableAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        return book != null && book.AvailableCopies > 0;
    }

    public async Task DecrementAvailableCopiesAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book != null && book.AvailableCopies > 0)
        {
            book.AvailableCopies--;
            await _context.SaveChangesAsync();
        }
    }

    public async Task IncrementAvailableCopiesAsync(int bookId)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book != null)
        {
            book.AvailableCopies++;
            await _context.SaveChangesAsync();
        }
    }


}

