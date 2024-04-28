using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet_Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserForPasswordEncryption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn("Password", "Users");
            migrationBuilder.AddColumn<byte[]>(//add instead of alter column because we droped the column
                name: "Password",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: "password"
                // oldClrType: typeof(string), dont need these two since we alreadyy dropped it
                // oldType: "text"
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordKey",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }
    }
}
