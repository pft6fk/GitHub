using GitHub.Models;
using GitHub.NewModels;
using Microsoft.EntityFrameworkCore;

namespace GitHub.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<ReposModel> Repos { get; set; }
        public DbSet<UserModelNew> Users{ get; set; }
        public DbSet<ContributorsModel> Contributors{ get; set; }
    }
}
