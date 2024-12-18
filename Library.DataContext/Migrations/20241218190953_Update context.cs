using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class Updatecontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                type: "nvarchar(89)",
                maxLength: 89,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(89)",
                oldMaxLength: 89,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                type: "nvarchar(89)",
                maxLength: 89,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(89)",
                oldMaxLength: 89);
        }
    }
}
