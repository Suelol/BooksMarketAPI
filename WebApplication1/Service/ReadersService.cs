using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class ReadersService : IReadersService
    {
        private readonly TestApiDb _context;

        public ReadersService(TestApiDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Readers>> GetAllReadersAsync()
        {
            return await _context.Readers.ToListAsync();
        }

        public async Task<Readers> GetReaderByIdAsync(int id)
        {
            return await _context.Readers.FindAsync(id);
        }

        public async Task<Readers> AddReaderAsync(Readers reader)
        {
            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public async Task<Readers> UpdateReaderAsync(int id, Readers reader)
        {
            if (id != reader.Id_Reader)
                return null;

            _context.Entry(reader).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ReaderExists(id))
                    return null;
                throw;
            }
            return reader;
        }

        public async Task<bool> DeleteReaderAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
                return false;

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Readers>> SearchReadersAsync(string searchTerm)
        {
            return await _context.Readers
                .Where(r => r.Name.Contains(searchTerm) ||
                            r.Surname.Contains(searchTerm) ||
                            r.Phone.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<(IEnumerable<Readers> readers, int totalCount)> GetReadersPaginatedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Readers.CountAsync();
            var readers = await _context.Readers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (readers, totalCount);
        }

        public async Task<int> GetTotalReadersCountAsync()
        {
            return await _context.Readers.CountAsync();
        }

        public async Task ImportReadersAsync(IEnumerable<Readers> readers)
        {
            await _context.Readers.AddRangeAsync(readers);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Readers>> ExportReadersAsync()
        {
            return await _context.Readers.ToListAsync();
        }

        private async Task<bool> ReaderExists(int id)
        {
            return await _context.Readers.AnyAsync(e => e.Id_Reader == id);
        }
    }
}
