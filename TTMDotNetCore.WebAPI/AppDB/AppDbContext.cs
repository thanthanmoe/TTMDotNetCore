using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TTMDotNetCore.WebAPI.Models;

namespace TTMDotNetCore.WebAPI.AppDB
{
    public class AppDbContext : DbContext
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TestDb",
            UserID = "sa",
            Password = "sasa",
            Encrypt=true,
            TrustServerCertificate = true
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if(optionsBuilder.IsConfigured == false)
            //{
            //}
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}


