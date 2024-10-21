using WebApplication1.Model;

namespace WebApplication1.Interface
{
    public interface IGenresService
    {
        Task<IEnumerable<Genres_Books>> GetAllGenresAsync();
        Task<Genres_Books> GetGenreByIdAsync(int id);
        Task<Genres_Books> AddGenreAsync(Genres_Books genre);
        Task<Genres_Books> UpdateGenreAsync(int id, Genres_Books genre);
        Task<bool> DeleteGenreAsync(int id);
        Task<IEnumerable<Genres_Books>> SearchGenresAsync(string searchTerm);
        Task<(IEnumerable<Genres_Books> genres, int totalCount)> GetGenresPaginatedAsync(int page, int pageSize);
        Task<int> GetTotalGenresCountAsync();
        Task ImportGenresAsync(IEnumerable<Genres_Books> genres);
        Task<IEnumerable<Genres_Books>> ExportGenresAsync();
    }
}
