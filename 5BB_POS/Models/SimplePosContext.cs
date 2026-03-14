using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _5BB_POS.Models;

public partial class SimplePosContext : DbContext
{
    public SimplePosContext()
    {
    }

    public SimplePosContext(DbContextOptions<SimplePosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategories { get; set; }
	public virtual DbSet<TblProduct> TblProducts { get; set; }
	public virtual DbSet<TblSale> TblSales { get; set; }
	public virtual DbSet<TblSaleItem> TblSaleItems { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("Tbl_Category");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });
		modelBuilder.Entity<TblProduct>(entity =>
		{
			entity.HasKey(e => e.ProductId);

			entity.ToTable("Tbl_Product");

			entity.Property(e => e.Name).HasMaxLength(150);
		});

		modelBuilder.Entity<TblSale>(entity =>
		{
			entity.HasKey(e => e.SaleId);

			entity.ToTable("Tbl_Sale"); 

		});
		modelBuilder.Entity<TblSaleItem>(entity =>
		{
			entity.HasKey(e => e.SaleItemId);
			entity.ToTable("Tbl_SaleItem");
		});
		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
