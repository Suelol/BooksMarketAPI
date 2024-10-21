using WebApplication1.Model;
using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IReadersService
    {
        Task<IEnumerable<Readers>> GetAllReadersAsync();
        Task<Readers> GetReaderByIdAsync(int id);
        Task<Readers> AddReaderAsync(Readers reader);
        Task<Readers> UpdateReaderAsync(int id, Readers reader);
        Task<bool> DeleteReaderAsync(int id);
        Task<IEnumerable<Readers>> SearchReadersAsync(string searchTerm);
        Task<(IEnumerable<Readers> readers, int totalCount)> GetReadersPaginatedAsync(int page, int pageSize);
        Task<int> GetTotalReadersCountAsync();
        Task ImportReadersAsync(IEnumerable<Readers> readers);
        Task<IEnumerable<Readers>> ExportReadersAsync();
    }
}
