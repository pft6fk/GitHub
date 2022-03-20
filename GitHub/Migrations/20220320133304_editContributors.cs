using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitHub.Migrations
{
    /// <inheritdoc />
    public partial class editContributors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributors_Repos_ReposId",
                table: "Contributors");

            migrationBuilder.DropIndex(
                name: "IX_Contributors_ReposId",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "ReposId",
                table: "Contributors");

            migrationBuilder.RenameColumn(
                name: "Contributors",
                table: "Contributors",
                newName: "NumberOfContributors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfContributors",
                table: "Contributors",
                newName: "Contributors");

            migrationBuilder.AddColumn<long>(
                name: "ReposId",
                table: "Contributors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ReposId",
                table: "Contributors",
                column: "ReposId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributors_Repos_ReposId",
                table: "Contributors",
                column: "ReposId",
                principalTable: "Repos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
