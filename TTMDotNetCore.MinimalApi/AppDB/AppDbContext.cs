using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TTMDotNetCore.MinimalApi.Models;

namespace TTMDotNetCore.MinimalApi.AppDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}


