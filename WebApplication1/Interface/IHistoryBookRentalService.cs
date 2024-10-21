using WebApplication1.Model;

namespace WebApplication1.Interface
{
    public interface IHistoryBookRentalService
    {
        Task<IEnumerable<History_Book_Rental>> GetAllHistoryAsync();
        Task<History_Book_Rental> GetHistoryByIdAsync(int id);
        Task<History_Book_Rental> AddHistoryAsync(History_Book_Rental history);
        Task<History_Book_Rental> UpdateHistoryAsync(int id, History_Book_Rental history);
        Task<bool> DeleteHistoryAsync(int id);
        Task<IEnumerable<History_Book_Rental>> SearchHistoryAsync(string searchTerm);
        Task<(IEnumerable<History_Book_Rental> histories, int totalCount)> GetHistoryPaginatedAsync(int page, int pageSize);
        Task<int> GetTotalHistoryCountAsync();
        Task ImportHistoryAsync(IEnumerable<History_Book_Rental> histories);
        Task<IEnumerable<History_Book_Rental>> ExportHistoryAsync();
    }
}
