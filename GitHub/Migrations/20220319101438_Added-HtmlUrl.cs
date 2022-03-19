using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHub.Migrations
{
    /// <inheritdoc />
    public partial class AddedHtmlUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlUrl",
                table: "Repos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlUrl",
                table: "Repos");
        }
    }
}
