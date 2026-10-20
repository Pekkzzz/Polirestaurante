using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoliRestaurante.Models.Entity;

namespace PoliRestaurante.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailStatus> OrderDetailStatuses { get; set; }

    public virtual DbSet<OrderModification> OrderModifications { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<PickupStatus> PickupStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<ToPickUp> ToPickUps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PoliRestaurante;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderStatusId, "fk_Order_OrderStatus1_idx");

            entity.HasIndex(e => e.OrderTypeId, "fk_Order_OrderType1_idx");

            entity.HasIndex(e => e.PaymentStatusId, "fk_Order_PaymentStatus1_idx");

            entity.HasIndex(e => e.TableId, "fk_Order_Table1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatus_ID");
            entity.Property(e => e.OrderTypeId).HasColumnName("OrderType_ID");
            entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatus_ID");
            entity.Property(e => e.Payments).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TableId).HasColumnName("Table_ID");
            entity.Property(e => e.TotalBalance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_OrderStatus1");

            entity.HasOne(d => d.OrderType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_OrderType1");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_PaymentStatus1");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("fk_Order_Table1");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "fk_OrderDetail_Order1_idx");

            entity.HasIndex(e => e.OrderDetailStatusId, "fk_OrderDetail_OrderDetailStatus1_idx");

            entity.HasIndex(e => e.ProductId, "fk_OrderDetail_Product1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Observations)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderDetailStatusId).HasColumnName("OrderDetailStatus_ID");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.OrderDetailStatus).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderDetailStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderDetail_OrderDetailStatus1");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderDetail_Order1");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderDetail_Product1");
        });

        modelBuilder.Entity<OrderDetailStatus>(entity =>
        {
            entity.ToTable("OrderDetailStatus");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderModification>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "fk_OrderModifications_Order1_idx");

            entity.HasIndex(e => e.UserId, "fk_OrderModifications_User1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderModifications)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderModifications_Order1");

            entity.HasOne(d => d.User).WithMany(p => p.OrderModifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderModifications_User1");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("OrderType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.ToTable("PaymentStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PickupStatus>(entity =>
        {
            entity.ToTable("PickupStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ProductTypeId, "fk_Product_ProductType1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_ProductType1");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.HasIndex(e => e.ReservationStatusId, "fk_Reservation_ReservationStatus1_idx");

            entity.HasIndex(e => e.TableId, "fk_Reservation_Table1_idx");

            entity.HasIndex(e => e.UserId, "fk_Reservation_User1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReservationStatusId).HasColumnName("ReservationStatus_ID");
            entity.Property(e => e.TableId).HasColumnName("Table_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.ReservationStatus).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReservationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Reservation_ReservationStatus1");

            entity.HasOne(d => d.Table).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Reservation_Table1");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Reservation_User1");
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.ToTable("ReservationStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.ToTable("Table");

            entity.HasIndex(e => e.TableNum, "TableNum_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<ToPickUp>(entity =>
        {
            entity.ToTable("ToPickUp");

            entity.HasIndex(e => e.OrderId, "fk_ToPickUp_Order1_idx");

            entity.HasIndex(e => e.PickupStatusId, "fk_ToPickUp_PickupStatus1_idx");

            entity.HasIndex(e => e.UserId, "fk_ToPickUp_User1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.PickupStatusId).HasColumnName("PickupStatus_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.ToPickUps)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ToPickUp_Order1");

            entity.HasOne(d => d.PickupStatus).WithMany(p => p.ToPickUps)
                .HasForeignKey(d => d.PickupStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ToPickUp_PickupStatus1");

            entity.HasOne(d => d.User).WithMany(p => p.ToPickUps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ToPickUp_User1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "Email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Username, "Username_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserRoleId, "fk_User_UserRole1_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserRoleId).HasColumnName("UserRole_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User_UserRole1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasIndex(e => e.Name, "Name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
