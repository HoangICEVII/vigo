﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vigo.Domain.AccountFolder;
using vigo.Domain.Entity;
using vigo.Domain.User;
using vigo.Infrastructure.Configuration;

namespace vigo.Infrastructure.DBContext
{
    public class VigoDatabaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DiscountCoupon> Discounts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomServiceR> RoomServices { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<ServiceR> Services { get; set; }
        public DbSet<ShowRoom> ShowRooms { get; set; }
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<SystemEmployee> SystemEmployees { get; set; }
        public DbSet<Tourist> Tourists { get; set; }

        public VigoDatabaseContext(DbContextOptions<VigoDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new BusinessPartnerConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountCouponConfiguration());

            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());

            modelBuilder.ApplyConfiguration(new ImageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            modelBuilder.ApplyConfiguration(new RoomServiceConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());

            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ShowRoomConfiguration());

            modelBuilder.ApplyConfiguration(new SystemEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new TouristConfiguration());

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { Id = 1, Name = "quản lí tài khoản", RoleLabel = "account_manage"},
                new RolePermission { Id = 2, Name = "quản lí đặt phòng", RoleLabel = "booking_manage" },
                new RolePermission { Id = 3, Name = "quản lí quyền", RoleLabel = "role_manage" },
                new RolePermission { Id = 4, Name = "quản lí giảm giá", RoleLabel = "discount_manage" },
                new RolePermission { Id = 5, Name = "quản lí giao dịch", RoleLabel = "transaction_manage" },
                new RolePermission { Id = 6, Name = "phản hồi, đánh giá", RoleLabel = "rating_manage" },
                new RolePermission { Id = 7, Name = "quản lí phòng", RoleLabel = "room_manage" },
                new RolePermission { Id = 8, Name = "quản lí dịch vụ", RoleLabel = "service_manage" },
                new RolePermission { Id = 9, Name = "quản lí chi nhánh", RoleLabel = "showroom_manage" }
            );

        }
    }
}
