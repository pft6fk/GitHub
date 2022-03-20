using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHub.Migrations
{
    /// <inheritdoc />
    public partial class remadestructure_addedonlyids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GitHubOwnerId",
                table: "Repos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubOwnerId",
                table: "Repos");
        }
    }
}
