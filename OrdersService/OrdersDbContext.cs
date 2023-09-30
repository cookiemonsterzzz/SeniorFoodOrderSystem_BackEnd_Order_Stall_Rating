using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrdersService.Models;

namespace OrdersService;

public partial class OrdersDbContext : DbContext
{
    public OrdersDbContext()
    {
    }

    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customerenquiry> Customerenquiries { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Foodscustomization> Foodscustomizations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Stall> Stalls { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=OrdersDb; Data Source=DESKTOP-QA7NAUN\\SQLEXPRESS; Persist Security Info=True;User ID=sa;Password=password;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customerenquiry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CustomerEnquiries");

            entity.ToTable("customerenquiries");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Enquiriesdescription)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("enquiriesdescription");
            entity.Property(e => e.Enquiriessubject)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("enquiriessubject");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Customerenquiries)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_CustomerEnquiries");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Foods");

            entity.ToTable("foods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Fooddescription)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("fooddescription");
            entity.Property(e => e.Foodname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("foodname");
            entity.Property(e => e.Foodprice)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("foodprice");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Stallid).HasColumnName("stallid");

            entity.HasOne(d => d.Stall).WithMany(p => p.Foods)
                .HasForeignKey(d => d.Stallid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Stall");
        });

        modelBuilder.Entity<Foodscustomization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FoodsCustomization");

            entity.ToTable("foodscustomization");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Foodcustomizationname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("foodcustomizationname");
            entity.Property(e => e.Foodcustomizationprice)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("foodcustomizationprice");
            entity.Property(e => e.Foodid).HasColumnName("foodid");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

            entity.HasOne(d => d.Food).WithMany(p => p.Foodscustomizations)
                .HasForeignKey(d => d.Foodid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Foods_FoodsCustomization");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Order");

            entity.ToTable("order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Foodcustomization)
                .HasMaxLength(100)
                .HasColumnName("foodcustomization");
            entity.Property(e => e.Foodname)
                .HasMaxLength(100)
                .HasColumnName("foodname");
            entity.Property(e => e.Foodprice)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("foodprice");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("orderdate");
            entity.Property(e => e.Orderdescription)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("orderdescription");
            entity.Property(e => e.Ordername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ordername");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("quantity");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Order");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Payment");

            entity.ToTable("payment");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("amount");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Stallid).HasColumnName("stallid");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Order");

            entity.HasOne(d => d.Stall).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Stallid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Stall");
        });

        modelBuilder.Entity<Stall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Stall");

            entity.ToTable("stall");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Stalldescription)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("stalldescription");
            entity.Property(e => e.Stallname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stallname");
            entity.Property(e => e.Stallowner)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stallowner");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_user");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Datetimecreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimecreated");
            entity.Property(e => e.Datetimeupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("datetimeupdated");
            entity.Property(e => e.Passcode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("passcode");
            entity.Property(e => e.Phoneno)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("phoneno");
            entity.Property(e => e.Roletype)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')")
                .HasColumnName("roletype");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<OrdersService.Models.Orders>? Orders_1 { get; set; }
}
