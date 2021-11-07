using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDistances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Distance = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDistances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Logitude = table.Column<double>(type: "REAL", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    DeliveryDistance = table.Column<double>(type: "REAL", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserTypes = table.Column<int>(type: "INTEGER", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    StateId = table.Column<string>(type: "TEXT", nullable: true),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryNo = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Read = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Riders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    BankCode = table.Column<string>(type: "TEXT", nullable: true),
                    BVN = table.Column<string>(type: "TEXT", nullable: true),
                    ValidID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Riders_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    LastSpend = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PickUpItems = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryTpe = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryTime = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Carriers = table.Column<int>(type: "INTEGER", nullable: false),
                    PickUpPhone = table.Column<string>(type: "TEXT", nullable: true),
                    DropOffPhone = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryLocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_DeliveryLocations_DeliveryLocationId",
                        column: x => x.DeliveryLocationId,
                        principalTable: "DeliveryLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_FileDatas_FileDataId",
                        column: x => x.FileDataId,
                        principalTable: "FileDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    AmountWithCharge = table.Column<decimal>(type: "TEXT", nullable: true),
                    DeliveriesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Deliveries_DeliveriesId",
                        column: x => x.DeliveriesId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiderGuarantors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RiderId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    NIN = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderGuarantors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiderGuarantors_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RidersDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryID = table.Column<int>(type: "INTEGER", nullable: false),
                    RiderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAsigned = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RidersDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RidersDeliveries_Deliveries_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RidersDeliveries_Riders_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Riders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Paid = table.Column<bool>(type: "INTEGER", nullable: false),
                    TransactionRef = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CountryId",
                table: "AppUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StateId",
                table: "AppUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_AppUserId",
                table: "Deliveries",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryId",
                table: "DeliveryItems",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryLocationId",
                table: "DeliveryItems",
                column: "DeliveryLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_FileDataId",
                table: "DeliveryItems",
                column: "FileDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppUserId",
                table: "Notifications",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppUserId",
                table: "Payments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_RiderGuarantors_RiderId",
                table: "RiderGuarantors",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Riders_AppUserId",
                table: "Riders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RidersDeliveries_DeliveryID",
                table: "RidersDeliveries",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_RidersDeliveries_RiderId",
                table: "RidersDeliveries",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DeliveriesId",
                table: "Transactions",
                column: "DeliveriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_AppUserId",
                table: "Wallets",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDistances");

            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RiderGuarantors");

            migrationBuilder.DropTable(
                name: "RidersDeliveries");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "DeliveryLocations");

            migrationBuilder.DropTable(
                name: "FileDatas");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Riders");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
