using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public class URLContextDBContext : DbContext
    {
        public DbSet<UrlMapping> UrlMappings { get; set; }

        public URLContextDBContext(DbContextOptions<URLContextDBContext> options)
            : base(options) { }
    }

}
