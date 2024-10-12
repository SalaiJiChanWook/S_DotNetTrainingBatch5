using DotNetTrainingBatch5.ConsoleAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DotNetTrainingBatch5.ConsoleAPP
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#; TrustServerCertificate=True;";//Certificate chain was issued by an authority that is not trusted ဆိုတဲ့ error မဖြစ်အောင်"TrustServerCertificate=True;"ထည့်ရမယ်။
                optionsBuilder.UseSqlServer(connectionString);
            }

            
        }

        public DbSet<BlogDataModel> Blogs { get; set; } 
    }
}
