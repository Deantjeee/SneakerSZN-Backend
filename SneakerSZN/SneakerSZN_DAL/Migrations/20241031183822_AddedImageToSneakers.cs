using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SneakerSZN_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageToSneakers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Sneakers",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sneakers");
        }
    }
}
