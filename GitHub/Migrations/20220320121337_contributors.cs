using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHub.Migrations
{
    /// <inheritdoc />
    public partial class contributors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contributors",
                table: "Contributors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contributors",
                table: "Contributors");
        }
    }
}
