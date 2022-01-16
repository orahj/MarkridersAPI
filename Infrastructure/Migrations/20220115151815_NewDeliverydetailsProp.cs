using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class NewDeliverydetailsProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Canceleduser",
                table: "DeliveryDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisputedComment",
                table: "DeliveryDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisputedUser",
                table: "DeliveryDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "DeliveryDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisputed",
                table: "DeliveryDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Canceleduser",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "DisputedComment",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "DisputedUser",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "DeliveryDetails");

            migrationBuilder.DropColumn(
                name: "IsDisputed",
                table: "DeliveryDetails");
        }
    }
}
