using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.MvcApp.Models;

namespace TTMDotNetCore.MvcApp.AppDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }


    }
}


