using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.ATMWebApp.Models;
namespace TTMDotNetCore.ATMWebApp.AppDB
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


