using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnModelBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Books",
                newName: "AuthorSecondName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Auhtors",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Books",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorSecondName",
                table: "Books",
                newName: "AuthorName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Auhtors",
                newName: "Name");
        }
    }
}
