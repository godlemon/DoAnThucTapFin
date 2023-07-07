using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnThucTapFin.Data.Migrations
{
    /// <inheritdoc />
    public partial class one3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "products",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "products");
        }
    }
}
