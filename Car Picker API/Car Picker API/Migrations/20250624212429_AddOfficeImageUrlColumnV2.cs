using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Picker_API.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficeImageUrlColumnV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeImageUrl",
                table: "Offices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeImageUrl",
                table: "Offices");
        }
    }
}
