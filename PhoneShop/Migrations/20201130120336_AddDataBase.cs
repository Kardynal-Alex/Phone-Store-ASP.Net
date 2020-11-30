using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneShop.Migrations
{
    public partial class AddDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date1",
                table: "PromoCodeSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date2",
                table: "PromoCodeSystems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "PromoCodeSystems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PromoCode",
                table: "PromoCodeSystems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date1",
                table: "PromoCodeSystems");

            migrationBuilder.DropColumn(
                name: "Date2",
                table: "PromoCodeSystems");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "PromoCodeSystems");

            migrationBuilder.DropColumn(
                name: "PromoCode",
                table: "PromoCodeSystems");
        }
    }
}
