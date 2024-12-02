using URL_Shortener.Data.Repositories;
using URL_Shortener.Models;

namespace URL_Shortener.Data.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UrlMapping> UrlMappingRepository { get; }
        Task SaveChangesAsync();
    }
}

