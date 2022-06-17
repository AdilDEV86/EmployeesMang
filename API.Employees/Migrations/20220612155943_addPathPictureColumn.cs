using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Employees.Migrations
{
    public partial class addPathPictureColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "TblEmployee",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "TblEmployee");
        }
    }
}
