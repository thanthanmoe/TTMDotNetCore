using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.ShoopingCartMvc.Models;

namespace TTMDotNetCore.ShoopingCartMvc.AppDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<AdminModel> Admins { get; set; }
    }
}


