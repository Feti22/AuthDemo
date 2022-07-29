using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthDemo.Data.Migrations
{
    public partial class NullableUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicule_AspNetUsers_UserId",
                table: "Vehicule");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vehicule",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicule_AspNetUsers_UserId",
                table: "Vehicule",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicule_AspNetUsers_UserId",
                table: "Vehicule");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Vehicule",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicule_AspNetUsers_UserId",
                table: "Vehicule",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
