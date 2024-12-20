﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vigo.Infrastructure.DBContext;

#nullable disable

namespace vigo.Infrastructure.Migrations
{
    [DbContext(typeof(VigoDatabaseContext))]
    [Migration("20241111084307_vigo")]
    partial class vigo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("vigo.Domain.AccountFolder.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.AccountFolder.EmailAuthen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ExprireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("emailAuthen", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.AccountFolder.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.AccountFolder.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoleLabel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("rolePermission", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "quản lí tài khoản",
                            RoleLabel = "account_manage"
                        },
                        new
                        {
                            Id = 2,
                            Name = "quản lí đặt phòng",
                            RoleLabel = "booking_manage"
                        },
                        new
                        {
                            Id = 3,
                            Name = "quản lí quyền",
                            RoleLabel = "role_manage"
                        },
                        new
                        {
                            Id = 4,
                            Name = "quản lí giảm giá",
                            RoleLabel = "discount_manage"
                        },
                        new
                        {
                            Id = 6,
                            Name = "phản hồi, đánh giá",
                            RoleLabel = "rating_manage"
                        },
                        new
                        {
                            Id = 7,
                            Name = "quản lí phòng",
                            RoleLabel = "room_manage"
                        },
                        new
                        {
                            Id = 8,
                            Name = "quản lí dịch vụ",
                            RoleLabel = "service_manage"
                        },
                        new
                        {
                            Id = 9,
                            Name = "quản lí tài khoản ngân hàng",
                            RoleLabel = "bank_manage"
                        });
                });

            modelBuilder.Entity("vigo.Domain.Entity.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("bank", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Approved")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("BusinessPartnerId")
                        .HasColumnType("int");

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("IsCheckOut")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TouristId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TouristId");

                    b.ToTable("booking", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.BusinessPartnerBank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("BankNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("BusinessPartnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("businessPartnerBank", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.DiscountCoupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessPartnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DiscountCount")
                        .HasColumnType("int");

                    b.Property<int>("DiscountMax")
                        .HasColumnType("int");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<decimal>("DiscountValue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoomApply")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("UserUsed")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("discount", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.District", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("district", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("image", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.Province", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("province", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TouristId")
                        .HasColumnType("int");

                    b.Property<string>("UpdateComment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TouristId");

                    b.ToTable("rating", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Avaiable")
                        .HasColumnType("int");

                    b.Property<int>("BookNumber")
                        .HasColumnType("int");

                    b.Property<int>("BusinessPartnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<decimal>("DefaultDiscount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DistrictId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoomTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Star")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("BusinessPartnerId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("room", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.RoomServiceR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("ServiceId");

                    b.ToTable("roomService", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("roomType", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.Entity.ServiceR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("service", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.User.BusinessPartner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BusinessKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("DistrictId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("businessPartner", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.User.SystemEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BankNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("systemEmployee", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.User.Tourist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("tourist", (string)null);
                });

            modelBuilder.Entity("vigo.Domain.AccountFolder.Account", b =>
                {
                    b.HasOne("vigo.Domain.AccountFolder.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.AccountFolder.EmailAuthen", b =>
                {
                    b.HasOne("vigo.Domain.AccountFolder.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.Booking", b =>
                {
                    b.HasOne("vigo.Domain.Entity.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vigo.Domain.User.Tourist", null)
                        .WithMany()
                        .HasForeignKey("TouristId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.District", b =>
                {
                    b.HasOne("vigo.Domain.Entity.Province", null)
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.Image", b =>
                {
                    b.HasOne("vigo.Domain.Entity.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.Rating", b =>
                {
                    b.HasOne("vigo.Domain.Entity.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vigo.Domain.User.Tourist", null)
                        .WithMany()
                        .HasForeignKey("TouristId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.Room", b =>
                {
                    b.HasOne("vigo.Domain.User.BusinessPartner", null)
                        .WithMany()
                        .HasForeignKey("BusinessPartnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vigo.Domain.Entity.RoomType", null)
                        .WithMany()
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.Entity.RoomServiceR", b =>
                {
                    b.HasOne("vigo.Domain.Entity.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("vigo.Domain.Entity.ServiceR", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.User.BusinessPartner", b =>
                {
                    b.HasOne("vigo.Domain.AccountFolder.Account", null)
                        .WithOne()
                        .HasForeignKey("vigo.Domain.User.BusinessPartner", "AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.User.SystemEmployee", b =>
                {
                    b.HasOne("vigo.Domain.AccountFolder.Account", null)
                        .WithOne()
                        .HasForeignKey("vigo.Domain.User.SystemEmployee", "AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("vigo.Domain.User.Tourist", b =>
                {
                    b.HasOne("vigo.Domain.AccountFolder.Account", null)
                        .WithOne()
                        .HasForeignKey("vigo.Domain.User.Tourist", "AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
