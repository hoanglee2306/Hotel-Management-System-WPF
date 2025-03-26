using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Models;

public partial class FUMiniHotelManagementContext : DbContext
{
    public FUMiniHotelManagementContext()
    {
    }

    public FUMiniHotelManagementContext(DbContextOptions<FUMiniHotelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<BookingReservation> BookingReservations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D47A6AFB413D");

            entity.Property(e => e.BookingDetailId).HasColumnName("BookingDetailID");
            entity.Property(e => e.ActualPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingReservationId).HasColumnName("BookingReservationID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.RoomPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.BookingReservation).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_BookingReservations");

            entity.HasOne(d => d.Room).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_Rooms");
        });

        modelBuilder.Entity<BookingReservation>(entity =>
        {
            entity.HasKey(e => e.BookingReservationId).HasName("PK__BookingR__A2B23EBF3DF14C85");

            entity.Property(e => e.BookingReservationId).HasColumnName("BookingReservationID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookingReservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingReservations_Customers");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8CF723078");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerFullName).HasMaxLength(50);
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__32863919C99E730E");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.RoomDescription).HasMaxLength(220);
            entity.Property(e => e.RoomNumber).HasMaxLength(50);
            entity.Property(e => e.RoomPricePerDate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_RoomTypes");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.RoomTypeId).HasName("PK__RoomType__BCC896116AE3FF8A");

            entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");
            entity.Property(e => e.RoomTypeName).HasMaxLength(50);
            entity.Property(e => e.TypeDescription).HasMaxLength(250);
            entity.Property(e => e.TypeNote).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
