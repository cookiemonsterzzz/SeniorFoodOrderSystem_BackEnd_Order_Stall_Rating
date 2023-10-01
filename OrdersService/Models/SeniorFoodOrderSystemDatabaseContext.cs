using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrdersService;

public partial class SeniorFoodOrderSystemDatabaseContext : DbContext
{
    public SeniorFoodOrderSystemDatabaseContext()
    {
    }

    public SeniorFoodOrderSystemDatabaseContext(DbContextOptions<SeniorFoodOrderSystemDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerEnquiry> CustomerEnquiries { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodsCustomization> FoodsCustomizations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Stall> Stalls { get; set; }

    public virtual DbSet<StallRating> StallRatings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEnquiry>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EnquiriesDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.EnquiriesSubject)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.CustomerEnquiries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_CustomerEnquiries");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FoodDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.FoodName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodPrice).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Stall).WithMany(p => p.Foods)
                .HasForeignKey(d => d.StallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Stall");
        });

        modelBuilder.Entity<FoodsCustomization>(entity =>
        {
            entity.ToTable("FoodsCustomization");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FoodCustomizationName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodCustomizationPrice).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodsCustomizations)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Foods_FoodsCustomization");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FoodCustomization).HasMaxLength(100);
            entity.Property(e => e.FoodName).HasMaxLength(100);
            entity.Property(e => e.FoodPrice).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OrderDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.OrderName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Order");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StallId).HasColumnName("StallID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Order");

            entity.HasOne(d => d.Stall).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Stall");
        });

        modelBuilder.Entity<Stall>(entity =>
        {
            entity.ToTable("Stall");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StallDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.StallName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StallOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StallRating>(entity =>
        {
            entity.ToTable("StallRating");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Review).HasMaxLength(1000);

            entity.HasOne(d => d.Stall).WithMany(p => p.StallRatings)
                .HasForeignKey(d => d.StallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StallRating_Stall");

            entity.HasOne(d => d.User).WithMany(p => p.StallRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StallRating_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DateTimeCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateTimeUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Passcode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.RoleType)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
