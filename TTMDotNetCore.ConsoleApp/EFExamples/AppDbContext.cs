using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TTMDotNetCore.ConsoleApp.Models;

namespace TTMDotNetCore.ConsoleApp.EFExamples
{
    public class AppDbContext : DbContext
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-F40FPLH",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "sasa"
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

