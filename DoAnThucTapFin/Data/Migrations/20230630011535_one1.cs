using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnThucTapFin.Data.Migrations
{
    /// <inheritdoc />
    public partial class one1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "products",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
