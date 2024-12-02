using URL_Shortener.Data.UnitofWork;
using URL_Shortener.Models;

namespace URL_Shortener.Services
{
    public class URLShortenerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public URLShortenerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetOrCreateShortUrlAsync(string longUrl)
        {
            var existing = await _unitOfWork.UrlMappingRepository.GetByConditionAsync(x => x.LongUrl == longUrl);
            if (existing != null)
            {
                return existing.ShortUrl;
            }

            var shortUrl = Guid.NewGuid().ToString().Substring(0, 8);
            var urlMapping = new UrlMapping { LongUrl = longUrl, ShortUrl = shortUrl };

            await _unitOfWork.UrlMappingRepository.AddAsync(urlMapping);
            await _unitOfWork.SaveChangesAsync();

            return shortUrl;
        }

        public async Task<string?> GetLongUrlAsync(string shortUrl)
        {
            var mapping = await _unitOfWork.UrlMappingRepository.GetByConditionAsync(x => x.ShortUrl == shortUrl);
            return mapping?.LongUrl;
        }
    }

}
