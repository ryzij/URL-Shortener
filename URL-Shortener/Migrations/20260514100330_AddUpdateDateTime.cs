using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Shortener.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "ShortUrls",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "ShortUrls");
        }
    }
}
