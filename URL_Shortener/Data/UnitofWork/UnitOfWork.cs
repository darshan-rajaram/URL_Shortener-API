using URL_Shortener.Data.Repositories;
using URL_Shortener.Models;

namespace URL_Shortener.Data.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly URLContextDBContext _context;
        private IRepository<UrlMapping>? _urlMappingRepository;

        public UnitOfWork(URLContextDBContext context)
        {
            _context = context;
        }

        public IRepository<UrlMapping> UrlMappingRepository
            => _urlMappingRepository ??= new Repository<UrlMapping>(_context);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
