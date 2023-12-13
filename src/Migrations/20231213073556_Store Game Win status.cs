using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSalad.Migrations
{
    /// <inheritdoc />
    public partial class StoreGameWinstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Won",
                table: "Games");
        }
    }
}
