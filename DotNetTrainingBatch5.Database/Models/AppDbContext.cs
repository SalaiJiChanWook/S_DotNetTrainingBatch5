using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        string connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#; TrustServerCertificate=True;";//Certificate chain was issued by an authority that is not trusted ဆိုတဲ့ error မဖြစ်အောင်"TrustServerCertificate=True;"ထည့်ရမယ်။
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<TblBlog>(entity =>
    //    {
    //        entity.HasKey(e => e.BlogId);

    //        entity.ToTable("Tbl_Blog");

    //        entity.Property(e => e.BlogAuthor).HasMaxLength(50);
    //        //entity.Property(e => e.BlogId).ValueGeneratedOnAdd();
    //        entity.Property(e => e.BlogTitle).HasMaxLength(50);
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}


    public virtual DbSet<TblAccounts> TblAccounts { get; set; }

   

    public virtual DbSet<TblTransactionRecord> TblTransactionHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=UCHIASALAI\\SQLEXPRESS;Database=DotNetTrainingBatch5;User Id=salai;Password=Vpjtqwv23@#;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            //entity.Property(e => e.BlogId).ValueGeneratedOnAdd();
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblAccounts>(entity =>
        {
            entity.ToTable("Tbl_Account");

            entity.Property(e => e.Balance).HasColumnType("decimal(25, 2)");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gmail).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.OTP);
            entity.Property(e => e.pin)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTransactionRecord>(entity =>
        {
            entity.HasKey(e => e.TranId);

            entity.ToTable("Tbl_TransactionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(25, 2)");
            entity.Property(e => e.FromMobileNo).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(150);
            entity.Property(e => e.ToMobileNo).HasMaxLength(50);
            entity.Property(e => e.TranDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

      

        OnModelCreatingPartial(modelBuilder);
    }




    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
