using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Picker_API.Migrations
{
    /// <inheritdoc />
    public partial class editeddatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewTitle",
                table: "OfficeReviews");

            migrationBuilder.DropColumn(
                name: "StarsReview",
                table: "OfficeReviews");

            migrationBuilder.DropColumn(
                name: "ReviewTitle",
                table: "CarReviews");

            migrationBuilder.DropColumn(
                name: "StarsReview",
                table: "CarReviews");

            migrationBuilder.AlterColumn<short>(
                name: "RoleId",
                table: "Users",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<short>(
                name: "RatingAmount",
                table: "OfficeReviews",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewContent",
                table: "CarReviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<short>(
                name: "RatingAmount",
                table: "CarReviews",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingAmount",
                table: "OfficeReviews");

            migrationBuilder.DropColumn(
                name: "RatingAmount",
                table: "CarReviews");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "ReviewTitle",
                table: "OfficeReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StarsReview",
                table: "OfficeReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ReviewContent",
                table: "CarReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewTitle",
                table: "CarReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StarsReview",
                table: "CarReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
