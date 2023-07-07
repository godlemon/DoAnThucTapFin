using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnThucTapFin.Data.Migrations
{
    /// <inheritdoc />
    public partial class one2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantitysell",
                table: "products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantitysell",
                table: "products");
        }
    }
}
