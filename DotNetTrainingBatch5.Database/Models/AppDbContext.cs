﻿using System;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source= UCHIASALAI\\SQLEXPRESS;Initial Catalog=DotNetTrainingBatch5;User ID=salai;Password=Vpjtqwv23@#; TrustServerCertificate=True;";//Certificate chain was issued by an authority that is not trusted ဆိုတဲ့ error မဖြစ်အောင်"TrustServerCertificate=True;"ထည့်ရမယ်။
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogId).ValueGeneratedOnAdd();
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}