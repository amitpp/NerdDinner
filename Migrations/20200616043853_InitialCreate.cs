using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdDinner.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dinner",
                columns: table => new
                {
                    DinnerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    EventDate = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    HostedBy = table.Column<string>(nullable: false),
                    ContactPhone = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dinner", x => x.DinnerID);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    LoginID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.LoginID);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    RegisterID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.RegisterID);
                });

            migrationBuilder.CreateTable(
                name: "Rsvp",
                columns: table => new
                {
                    RsvpID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DinnerID = table.Column<int>(nullable: false),
                    AttendeeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rsvp", x => x.RsvpID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dinner");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Rsvp");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
