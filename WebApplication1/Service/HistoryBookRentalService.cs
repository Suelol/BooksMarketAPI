using Microsoft.EntityFrameworkCore;
using WebApplication1.DbContextApi;
using WebApplication1.Interface;
using WebApplication1.Model;

namespace WebApplication1.Service
{
    public class HistoryBookRentalService : IHistoryBookRentalService
    {
        private readonly TestApiDb _context;

        public HistoryBookRentalService(TestApiDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<History_Book_Rental>> GetAllHistoryAsync()
        {
            return await _context.History_Book_Rentals.ToListAsync();
        }

        public async Task<History_Book_Rental> GetHistoryByIdAsync(int id)
        {
            return await _context.History_Book_Rentals.FindAsync(id);
        }

        public async Task<History_Book_Rental> AddHistoryAsync(History_Book_Rental history)
        {
            _context.History_Book_Rentals.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<History_Book_Rental> UpdateHistoryAsync(int id, History_Book_Rental history)
        {
            if (id != history.Id)
                return null;

            _context.Entry(history).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HistoryExists(id))
                    return null;
                throw;
            }
            return history;
        }

        public async Task<bool> DeleteHistoryAsync(int id)
        {
            var history = await _context.History_Book_Rentals.FindAsync(id);
            if (history == null)
                return false;

            _context.History_Book_Rentals.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<History_Book_Rental>> SearchHistoryAsync(string searchTerm)
        {
            return await _context.History_Book_Rentals
                .Where(h => h.Books.Title.Contains(searchTerm) || h.Readers.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<(IEnumerable<History_Book_Rental> histories, int totalCount)> GetHistoryPaginatedAsync(int page, int pageSize)
        {
            var totalCount = await _context.History_Book_Rentals.CountAsync();
            var histories = await _context.History_Book_Rentals
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (histories, totalCount);
        }

        public async Task<int> GetTotalHistoryCountAsync()
        {
            return await _context.History_Book_Rentals.CountAsync();
        }

        public async Task ImportHistoryAsync(IEnumerable<History_Book_Rental> histories)
        {
            await _context.History_Book_Rentals.AddRangeAsync(histories);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<History_Book_Rental>> ExportHistoryAsync()
        {
            return await _context.History_Book_Rentals.ToListAsync();
        }

        private async Task<bool> HistoryExists(int id)
        {
            return await _context.History_Book_Rentals.AnyAsync(e => e.Id == id);
        }
    }
}
