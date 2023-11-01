using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kayıtlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kayıtlar", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Kayıtlar",
                columns: new[] { "Id", "Address", "Email", "FullName", "Phone" },
                values: new object[] { 1, "bağdat caddesi no 41", "gorkeme@monster.com", "görkem görkem", 1235465L });

            migrationBuilder.InsertData(
                table: "Kayıtlar",
                columns: new[] { "Id", "Address", "Email", "FullName", "Phone" },
                values: new object[] { 2, "kosuyolu caddesi no 41", "johndoe@monster.com", "john doe", 1995544L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kayıtlar");
        }
    }
}
