using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simp.Modules.Blogs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Blog_Add_IsTemplate_ContentHtml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentHtml",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTemplate",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentHtml",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsTemplate",
                table: "Blogs");
        }
    }
}
