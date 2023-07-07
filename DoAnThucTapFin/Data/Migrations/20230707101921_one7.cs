using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnThucTapFin.Data.Migrations
{
    /// <inheritdoc />
    public partial class one7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusId",
                value: 0);
        }
    }
}
