using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class locationAsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryLocations_DeliveryItems_DeliveryItemId",
                table: "DeliveryLocations");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryLocations_DeliveryItemId",
                table: "DeliveryLocations");

            migrationBuilder.DropColumn(
                name: "DeliveryItemId",
                table: "DeliveryLocations");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryLocationId",
                table: "DeliveryItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryLocationId",
                table: "DeliveryItems",
                column: "DeliveryLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_DeliveryLocations_DeliveryLocationId",
                table: "DeliveryItems",
                column: "DeliveryLocationId",
                principalTable: "DeliveryLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_DeliveryLocations_DeliveryLocationId",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_DeliveryLocationId",
                table: "DeliveryItems");

            migrationBuilder.DropColumn(
                name: "DeliveryLocationId",
                table: "DeliveryItems");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryItemId",
                table: "DeliveryLocations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryLocations_DeliveryItemId",
                table: "DeliveryLocations",
                column: "DeliveryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryLocations_DeliveryItems_DeliveryItemId",
                table: "DeliveryLocations",
                column: "DeliveryItemId",
                principalTable: "DeliveryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
