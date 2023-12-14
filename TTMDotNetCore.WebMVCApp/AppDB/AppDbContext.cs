using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.WebMVCApp.Models;

namespace TTMDotNetCore.WebMVCApp.AppDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }


    }
}


