using Microsoft.EntityFrameworkCore;
using NguyenMinhDuyThong.Models;

namespace NguyenMinhDuyThong
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        public DbSet<PhoneModels> Phones { get; set; }
        
    }

}
