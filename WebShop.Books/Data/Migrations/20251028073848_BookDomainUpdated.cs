using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Books.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookDomainUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "Books",
                table: "Books",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "Books",
                table: "Books",
                newName: "UnitPrice");
        }
    }
}
