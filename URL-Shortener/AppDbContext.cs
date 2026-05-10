using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
        public DbSet<ClickInfo> ClickInfos => Set<ClickInfo>();
        public DbSet<User> Users => Set<User>();
    }
}
