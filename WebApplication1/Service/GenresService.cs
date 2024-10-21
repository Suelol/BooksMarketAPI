using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

namespace WebApplication1.Service
{
    public class GenresService : IGenresService
    {
        private readonly TestApiDb _context;

        public GenresService(TestApiDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Genres_Books>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genres_Books> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genres_Books> AddGenreAsync(Genres_Books genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genres_Books> UpdateGenreAsync(int id, Genres_Books genre)
        {
            if (id != genre.Id_Genres_Books)
                return null;

            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Genres_Books>> SearchGenresAsync(string searchTerm)
        {
            return await _context.Genres
                .Where(g => g.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<(IEnumerable<Genres_Books> genres, int totalCount)> GetGenresPaginatedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Genres.CountAsync();
            var genres = await _context.Genres
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (genres, totalCount);
        }

        public async Task<int> GetTotalGenresCountAsync()
        {
            return await _context.Genres.CountAsync();
        }

        public async Task ImportGenresAsync(IEnumerable<Genres_Books> genres)
        {
            await _context.Genres.AddRangeAsync(genres);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Genres_Books>> ExportGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}
