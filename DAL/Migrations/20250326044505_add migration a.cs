using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CustomerBirthday = table.Column<DateOnly>(type: "date", nullable: true),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8CF723078", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TypeNote = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoomType__BCC896116AE3FF8A", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BookingReservations",
                columns: table => new
                {
                    BookingReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CheckinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckoutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingR__A2B23EBF3DF14C85", x => x.BookingReservationID);
                    table.ForeignKey(
                        name: "FK_BookingReservations_Customers",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomDescription = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: true),
                    RoomMaxCapacity = table.Column<int>(type: "int", nullable: false),
                    RoomStatus = table.Column<int>(type: "int", nullable: false),
                    RoomPricePerDate = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rooms__32863919C99E730E", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID");
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingReservationID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    RoomPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ActualPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingD__8136D47A6AFB413D", x => x.BookingDetailID);
                    table.ForeignKey(
                        name: "FK_BookingDetails_BookingReservations",
                        column: x => x.BookingReservationID,
                        principalTable: "BookingReservations",
                        principalColumn: "BookingReservationID");
                    table.ForeignKey(
                        name: "FK_BookingDetails_Rooms",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingReservationID",
                table: "BookingDetails",
                column: "BookingReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_RoomID",
                table: "BookingDetails",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservations_CustomerID",
                table: "BookingReservations",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeID",
                table: "Rooms",
                column: "RoomTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "BookingReservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
