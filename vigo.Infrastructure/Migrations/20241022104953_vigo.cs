using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace vigo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class vigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imageType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_province", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Permission = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolePermission", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roomType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "district",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_district_province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "province",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "businessPartner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_businessPartner_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "systemEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BankNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_systemEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_systemEmployee_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tourist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tourist_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "account",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "showRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    BusinessPartnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_showRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_showRoom_businessPartner_BusinessPartnerId",
                        column: x => x.BusinessPartnerId,
                        principalTable: "businessPartner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_showRoom_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_showRoom_province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "province",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DiscountMax = table.Column<int>(type: "int", nullable: false),
                    DiscountCount = table.Column<int>(type: "int", nullable: false),
                    UserUsed = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoomApply = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountApply = table.Column<int>(type: "int", nullable: false),
                    ShowRoomId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_discount_showRoom_ShowRoomId",
                        column: x => x.ShowRoomId,
                        principalTable: "showRoom",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Thumbnail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShowRoomId = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Avaiable = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_room_roomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "roomType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_room_showRoom_ShowRoomId",
                        column: x => x.ShowRoomId,
                        principalTable: "showRoom",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TouristId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DiscountCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiscountPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_booking_tourist_TouristId",
                        column: x => x.TouristId,
                        principalTable: "tourist",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ImageTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_image_imageType_ImageTypeId",
                        column: x => x.ImageTypeId,
                        principalTable: "imageType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_image_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TouristId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateComment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rating_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rating_tourist_TouristId",
                        column: x => x.TouristId,
                        principalTable: "tourist",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roomService",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_roomService_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_roomService_service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "service",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "booking",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "rolePermission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "quản lí tài khoản" },
                    { 2, "quản lí đặt phòng" },
                    { 3, "quản lí quyền" },
                    { 4, "quản lí giảm giá" },
                    { 5, "kiểu ảnh" },
                    { 6, "quản lí giao dịch" },
                    { 7, "phản hồi, đánh giá" },
                    { 8, "quản lí phòng" },
                    { 9, "quản lí dịch vụ" },
                    { 10, "quản lí chi nhánh" },
                    { 11, "quản lí thông tin cá nhân" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_RoleId",
                table: "account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_RoomId",
                table: "booking",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_TouristId",
                table: "booking",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_businessPartner_AccountId",
                table: "businessPartner",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_discount_ShowRoomId",
                table: "discount",
                column: "ShowRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_district_ProvinceId",
                table: "district",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_image_ImageTypeId",
                table: "image",
                column: "ImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_image_RoomId",
                table: "image",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_BookingId",
                table: "invoice",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rating_RoomId",
                table: "rating",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_TouristId",
                table: "rating",
                column: "TouristId");

            migrationBuilder.CreateIndex(
                name: "IX_room_RoomTypeId",
                table: "room",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_room_ShowRoomId",
                table: "room",
                column: "ShowRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_roomService_RoomId",
                table: "roomService",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_roomService_ServiceId",
                table: "roomService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_showRoom_BusinessPartnerId",
                table: "showRoom",
                column: "BusinessPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_showRoom_DistrictId",
                table: "showRoom",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_showRoom_ProvinceId",
                table: "showRoom",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_systemEmployee_AccountId",
                table: "systemEmployee",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tourist_AccountId",
                table: "tourist",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "rating");

            migrationBuilder.DropTable(
                name: "rolePermission");

            migrationBuilder.DropTable(
                name: "roomService");

            migrationBuilder.DropTable(
                name: "systemEmployee");

            migrationBuilder.DropTable(
                name: "imageType");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "service");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "tourist");

            migrationBuilder.DropTable(
                name: "roomType");

            migrationBuilder.DropTable(
                name: "showRoom");

            migrationBuilder.DropTable(
                name: "businessPartner");

            migrationBuilder.DropTable(
                name: "district");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "province");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
