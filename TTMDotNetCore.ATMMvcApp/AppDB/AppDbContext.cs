using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.ATMMvcApp.Models;
namespace TTMDotNetCore.ATMMvcApp.AppDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
		public DbSet<BlogDataModel> Blogs { get; set; }

		public DbSet<UserModel> Users { get; set; }

		public DbSet<AdminModel> Admins { get; set; }
	}
}


