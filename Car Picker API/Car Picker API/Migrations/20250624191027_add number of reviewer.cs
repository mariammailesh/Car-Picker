using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Picker_API.Migrations
{
    /// <inheritdoc />
    public partial class addnumberofreviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalReviewsForOffice",
                table: "OfficeReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReviewsForOffice",
                table: "OfficeReviews");
        }
    }
}
