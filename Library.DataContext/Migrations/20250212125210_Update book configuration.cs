using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class Updatebookconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorSecondName",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Books",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorSecondName",
                table: "Books",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
