using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Bill_Id",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscription_Bill_MemberId",
                table: "MemberSubscription");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "MemberSubscription",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSubscription_BillId",
                table: "MemberSubscription",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BillId",
                table: "Booking",
                column: "BillId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Bill_BillId",
                table: "Booking",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscription_Bill_BillId",
                table: "MemberSubscription",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Bill_BillId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSubscription_Bill_BillId",
                table: "MemberSubscription");

            migrationBuilder.DropIndex(
                name: "IX_MemberSubscription_BillId",
                table: "MemberSubscription");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BillId",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "MemberSubscription",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BillId",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Bill_Id",
                table: "Booking",
                column: "Id",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSubscription_Bill_MemberId",
                table: "MemberSubscription",
                column: "MemberId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
